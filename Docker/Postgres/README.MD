# Instruções

Nesta pasta existem scripts e arquivos para gerar imagens e containers de uso para o PostgreSql.

# Comandos úteis

### Para construir imagem a partir do dockerfile
`docker build -t leokporto/actionnetpostgredb:latest .`

### Para criar o container
`docker run -p 15432:5432 -d leokporto/actionnetpostgredb:latest`

### Para criar um container a partir do docker compose
`docker compose -p NomeProjeto up`

# Instruções PgAdmin

Para acessar o pgAdmin use o usuário `admin@email.com`, a senha da criação do container.

Para adicionar o servidor ao pgAdmin, use como endereço de conexão o nome do container do banco pgSql criado. Por exemplo, se o nome do projeto passado no comando do docker compose for testedb, o nome do container provavelmente será testedbPgSql.