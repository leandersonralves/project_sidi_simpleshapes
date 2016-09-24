# MANUAL GAME - SIMPLE SHAPES #

Este jogo, como o nome já transpõe, é bem simples.

O Objetivo é dirigir uma SUV fugindo de outros motoristas barbeiros, que irão atrás de você.
Caso bata demais o seu veículo ou os outros batam demais, o carro irá quebrar e soltar fumaça.

### Instação: ###
1. Baixe o .apk neste repositório, instale no celular Android.
2. Divirta-se.

Em todas as fases possui um **botão no canto superior direito** para acesso rápido à **outras telas**.
A primeira é um jogo simples, as outras duas, servem apenas para demonstração dos shaders desenvolvidos.

Para pilotar a SUV use os **sliders** laterais.
O slider da _esquerda_ controla a _aceleração_ frontal/ré.
O slider da _direita_ controla a _direção_.

### Problemas conhecidos: ###
* O projeto foi desenvolvido em dois ambientes distintos, *Windows 10* e *Ubuntu 16.04 LTS*. Ocorreram problemas quando o projeto era atualizado em um sistema e em seguida aberto em outro, ocorriam *perda de referências* dos scripts, perda de referências dos scripts em Prefabs e até mesmo de Prefabs filhos com o Parent. Alterado o modo do Unity salvas os assets, para que *serialize* em formato *string* ao invés de *binário*, amenizou o problema, porém não sanou totalmente.