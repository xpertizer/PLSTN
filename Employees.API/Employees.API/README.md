# PLSTN API - API para calculo do pagamento da divisao de lucros

Documentação consultar o Swagger da aplicação.

A aplicação utiliza mongodb em Cloud. Informações sobre os dados para conexão vide appsettings.json seção "DatabaseSettings"

Abaixo o descritivo do desafio.

Duvidas enviar mensagem via Whatsapp para 21 970164450.

Descrição

Uma empresa fechou o ano de operação com lucro e deseja reparti-lo entre seus funcionários, com o objetivo de ser justa criou uma regra para a distribuição deste montante por: área, tempo de empresa, e faixa salarial (os funcionários que ganham menos teriam sua participação incrementada).
Para isso foi solicitado ao time de tecnologia que desenvolva uma API REST, que receba o valor máximo a distribuir, efetue a distribuição dentro do possível para os funcionários já cadastrados, aplicando um conjunto de regras pré-estabelecidas, atentando-se que pode não ser possível distribuir o valor disponibilizado, ou seja, o montante disponibilizado pode ser insuficiente.
As regras de distribuição são:

Regras Gerais

Foi estabelecido um peso por área de atuação:
Peso 1: Diretoria;
Peso 2: Contabilidade, Financeiro, Tecnologia; Peso 3: Serviços Gerais;
Peso 5: Relacionamento com o Cliente;

Foi estabelecido um peso por faixa salarial e uma exceção para estagiários:
Peso 5: Acima de 8 salários mínimos;
Peso 3: Acima de 5 salários mínimos e menor que 8 salários mínimos;
Peso 2: Acima de 3 salários mínimos e menor que 5 salários mínimos;
Peso 1: Todos os estagiários e funcionários que ganham até 3 salários mínimos;

Foi estabelecido um peso por tempo de admissão:
Peso 1: Até 1 ano de casa;
Peso 2: Mais de 1 ano e menos de 3 anos; Peso 3: Acima de 3 anos e menos de 8 anos; Peso 5: Mais de 8 anos


Pelas regras estabelecidas a fórmula para se chegar ao bônus de cada funcionário é:

(SB * PTA) + (SB * PAA)
______________________________  * 12 (Meses do Ano)
(PFS)
Legenda
SB: Salário Bruto
PTA: Peso por tempo de admissão
PAA: Peso por aréa de atuação
PFS: Peso por faixa salarial