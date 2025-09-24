# Complexity Calculator

## Descrição
**Complexity Calculator** é um projeto bem simples em **C# e WPF** que permite analisar a complexidade de algoritmos em código C#.  
O usuário pode colar ou digitar o código diretamente em um **TextBox**, e o sistema calcula uma estimativa da complexidade em notação **Big O**, levando em consideração:

- Profundidade de loops (`for`, `while`, `foreach`)
- Presença de recursão

O projeto utiliza **Roslyn** para analisar a sintaxe do código e detectar padrões que influenciam a complexidade. Ainda tem mui8to a acrescentar e incrementar para atender a cenários mais complexos, por exmeplo algoritmos de ordenação e pesquisa mais avançados, como bublesort, quicksort, etc, mas para algoritmos simples, funciuona bem. Um dos objetivos é o uso para auxílio na análise do algoritmo em busca de gargalos de lentidão.

---

## Funcionalidades
- Entrada de código C# via interface WPF.
- Estimativa automática da complexidade do algoritmo.
- Identificação de recursão.
- Cálculo da profundidade de loops aninhados.
- Exibição de resultado em notação Big O.

---

## Instalação

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/ComplexityCalculator.git
cd ComplexityCalculator
