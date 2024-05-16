**Objetivo**

Criar uma API para listar a posição atual de ações de uma carteira.
Para isso será necessário consultar o valor das ações do dia na API fornecida. 
Utilizar o Mockoon para disponibilizá-la (StockPrices_MockoonEnv.json).

Criar um serviço de domínio que consulte a API fornecida e forneça os dados para a aplicação.
É ideal que se utilize cache para não sobrecarregar a API externa.

GET `/api/wallet/{walletId}/stocks-position`

```
[
    {
       "code": <código da ação>
       "amount": <quantidade de ações na carteira>
       "valuePerQuota": <valor do dia>
       "total": <valor da posicição atual>
    }
]
```
**Critérios de Aceite**

- Deve listar o valor total da ação na carteira
- O consumo da API externa deve ser resiliente e tentar acioná-la novamente até 3 vezes com um intervalo de 5 segundos entre elas.
- Em caso de erro, retornar o status code 500 com uma mensagem amigável ao usuário

**Critério de Desenvolvimento**

- Deve utilizar o cache distribuído do ASP.NET