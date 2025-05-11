# Documentação da API de Animes 🌟

API REST desenvolvida para gerenciamento de animes, contendo operações de CRUD com filtros, paginação e exclusão lógica.

---

## 🔍 GET /api/anime

### Descrição:

Retorna todos os animes ativos. Suporta filtros por nome, diretor, resumo e paginação.

### Parâmetros opcionais (query string):

* `nome`: string
* `diretor`: string
* `resumo`: string
* `ativo`: bool

### Exemplo de resposta:

```json
[
  {
    "id": 1,
    "nome": "Elfen Lied",
    "resumo": "Anjos com poderes e violência psicológica.",
    "diretor": "Mamoru Kanbe"    "ativo": true
  }
]
```

---

## 🔍 GET /api/anime/{id}

### Descrição:

Retorna o anime com o ID fornecido (desde que esteja ativo).

### Exemplo de resposta:

```json
{
  "id": 1,
  "nome": "Mirai Nikki",
  "resumo": "Diários do futuro e sobrevivência.",
  "diretor": "Naoto Hosoda"
}
```

---

## ✅ POST /api/anime

### Descrição:

Cria um novo anime. O campo `ativo` é definido como `true` automaticamente.

### Exemplo de requisição:

```json
{
  "nome": "Attack on Titan",
  "resumo": "Humanidade contra titãs.",
  "diretor": "Tetsurō Araki"
}
```

### Exemplo de resposta:

```json
{
  "id": 3,
  "nome": "Attack on Titan",
  "resumo": "Humanidade contra titãs.",
  "diretor": "Tetsurō Araki"
}
```

---

## ✏️ PUT /api/anime/{id}

### Descrição:

Atualiza os dados de um anime existente.

### Exemplo de requisição:

```json
{
  "nome": "Shingeki no Kyojin",
  "resumo": "Versão atualizada do resumo.",
  "diretor": "Araki"
}
```

### Exemplo de resposta:

```json
{ "mensagem": "Anime com ID 3 atualizado com sucesso!" }
```

---

## ❌ DELETE /api/anime/{id}

### Descrição:

Desativa (exclusão lógica) o anime com o ID informado.

### Exemplo de resposta:

```json
{ "mensagem": "Anime 'Shingeki no Kyojin' desativado com sucesso." }
```

---

## ⚠️ DELETE /api/anime/delete-all

### Descrição:

Deleta todos os animes do banco (exclusão física). **Somente executa se o arquivo `C:\TEMP\elias.txt` existir.**

### Exemplo de resposta:

```json
{ "mensagem": "Todos os animes foram deletados com sucesso!" }
```

---

## 📈 Prints do Swagger

Adicione imagens mostrando testes dos endpoints no Swagger, como:

* Criação de anime
* Atualização
* Consulta com filtros
* Exclusão lógica

---

## 🔧 Testes Automatizados

Testes realizados com xUnit + EF Core InMemory:

* Criação de anime
* Consulta por ID
* Atualização de anime
* Exclusão lógica
* Listagem com filtros e paginação

---

## ⚛️ Tecnologias Utilizadas

* ASP.NET Core 8
* Entity Framework Core
* PostgreSQL
* xUnit
* Swagger (Swashbuckle)

---

Desenvolvido por Elias ✨
