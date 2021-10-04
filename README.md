# CRUD
Para usar a API é necessário autenticar via JWT, usando Bearer Token, usando o Postman ou similares.
As credenciais para geração do token são esperadas no formato:
###
`{
    "email": "email@teste",
    "senha": "123456"
}`
#
### Observação
A persistencia de dados sensíveis como senhas e tokens é feita via hash no banco.

No arquivo database_create.sql temos a criação do banco para SQL Server juntamente com as tabelas necessárias e o primeiro usuário para que se possar usar a API.
#
### Pagina de manipulação da API (CRUD User)

https://localhost:44369/api/user
#
**POST** : Insert
###
`
{
    "email": "email@teste",
    "senha": "123456"
}
`
#
**Get user por id**
###
https://localhost:44369/api/user/id : Get de usuario unico passado no lugar de id
#
**PUT** : Update (não altera senha)
###
`
{
    "id": 2,
    "nome": "MESTRE777",
    "senha": "******",
    "email": "email@teste"
}
`
#
**Delete** : Delete
###
Passar o id do user a ser deletado no lugar de id
###

https://localhost:44369/api/user/id
#
# Alteração de senha

### Solicitar token
Substituir id pelo id do usuario que se deseja alterar a senha
###

https://localhost:44369/api/ChangePassword/id

###
Irá retornar o token necessário para alteração da senha
###
### Alteração de senha com o token fornecido

PUT em https://localhost:44369/api/ChangePassword com JSON no body no formato
###
`{
    "id": 5,
    "nome": "MESTRE",
    "senha": "123456",
    "email": "email@teste",
    "senhaNova":"123456senha_nova",
    "token":"M0KKTM"
}`
###
### Solicitação do Bearer Token
Solicitar na pagina https://localhost:44369/api/token passando email e senha como parametros no JSON
###
`{
    "email": "email@teste",
    "senha": "123456"
}`
