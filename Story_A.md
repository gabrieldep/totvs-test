**Objetivo**

Cada investimento possui uma descrição padrão que permite identificar a operação. Como o sistema será utilizado por usuários
de várias localidades do mundo, essa descrição deve ser traduzida para outros idiomas.

Já possuímos uma API para atualização dessas descrições, mas é necessário evoluir a estrutura do banco de dados e a
camanada de aplicação para armazenar corretamente.

PUT `/api/investiments/{investimentId}/localizations`
```
[
  {
    "languageCode": "string",
    "description": "string"
  }
]
```
**Critérios de Aceite**

- Deve armazenar as traduções da descrição de um investimento corretamente
- Somente aceitar o `languageCode` restrito às linguas suportadas: *pt*, *en*, *es*
- A `description` não deve ter menos que 10 caracteres

**Critérios de Desenvolvimento**

- Utilizar a biblioteca FluentValidations, já disponível na solução. 