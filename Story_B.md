**Objetivo**

Criar uma API para listar todos os investimentos presentes em uma carteira.

GET `/api/wallets/{walletId}/investments`

**Critérios de Aceite**

- Deve listar todos os investimentos de uma carteira
- A descrição do investimeto deve respeitar o idioma utilizado na requisição. 
  - Utilizar o header *accept-language* para ter acesso a informação necessária.