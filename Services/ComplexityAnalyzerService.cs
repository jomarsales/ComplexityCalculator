using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComplexityCalculator.Services;

public class ComplexityAnalyzerService
{
    /// <summary>
    /// Calcula a complexidade de um código C# informado.
    /// </summary>
    public string Analyze(string code)
    {
        if (!code.Contains("class"))
        {
            code = $@"public class DummyClass {{
                         {code}
                      }}";
        }
        
        var tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetRoot();

        var compilation = CSharpCompilation.Create("Analysis").AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(tree);

        var semanticModel = compilation.GetSemanticModel(tree);

        var maxLoopNesting = GetMaxLoopNesting(root);
        var recursion = DetectRecursion(root, semanticModel);

        return EstimateComplexity(maxLoopNesting, recursion);
    }

    /// <summary>
    /// Retorna a profundidade máxima de loops (for, while, foreach).
    /// </summary>
    private static int GetMaxLoopNesting(SyntaxNode node, int current = 0)
    {
        var max = current;

        foreach (var child in node.ChildNodes())
        {
            var increment = 0;
           
            if (child is ForStatementSyntax or WhileStatementSyntax or ForEachStatementSyntax)
            {
                increment = 1;
            }

            var childMax = GetMaxLoopNesting(child, current + increment);
            
            if (childMax > max) max = childMax;
        }

        return max;
    }

    /// <summary>
    /// Detecta se existe recursão.
    /// </summary>
    private static bool DetectRecursion(SyntaxNode root, SemanticModel semanticModel)
    {
        var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

        foreach (var method in methods)
        {
            var methodSymbol = semanticModel.GetDeclaredSymbol(method);
            
            if (methodSymbol == null) continue;

            var calls = method.DescendantNodes().OfType<InvocationExpressionSyntax>();

            foreach (var call in calls)
            {
                var callSymbol = semanticModel.GetSymbolInfo(call).Symbol as IMethodSymbol;
                
                if (callSymbol == null) continue;

                if (SymbolEqualityComparer.Default.Equals(methodSymbol, callSymbol))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Retorna a complexidade estimada em notação Big O.
    /// </summary>
    private static string EstimateComplexity(int loopNesting, bool recursion)
    {
        if (recursion)
            return "O(2^n) (recursão detectada)";

        return loopNesting switch
        {
            > 1 => $"O(n^{loopNesting})",
            1 => "O(n)",
            _ => "O(1)"
        };
    }
}