# Documentação da API de Criptografia

## Introdução

A API de Criptografia foi desenvolvida com o objetivo de proporcionar uma camada adicional de segurança aos dados sensíveis armazenados em um banco de dados. A implementação utiliza a criptografia em tempo de execução para garantir que campos sensíveis, como `UserDocument` e `CreditCardToken`, não sejam visíveis diretamente.

## Exemplo de Uso

Considere a tabela de exemplo a seguir:

| id | UserDocument     | CreditCardToken | value |
|:---|:-----------------|:----------------|:------|
| 1  | MzYxNDA3ODE4MzM= | YWJjMTIz        | 5999  |
| 2  | MzI5NDU0MTA1ODM= | eHl6NDU2        | 1000  |
| 3  | NzYwNzc0NTIzODY= | Nzg5eHB0bw==    | 1500  |

A estrutura da entidade correspondente:

| Campo           | Tipo   |
|:----------------|:-------|
| id              | Long   |
| UserDocument    | String |
| CreditCardToken | String |
| value           | Double |

## Endpoints

### 1. Obter todos os dados sensíveis

```http
GET /api/SensitiveData
```

#### Resposta de Sucesso

```json
[
  {
    "id": 1,
    "UserDocument": "123456789",
    "CreditCardToken": "abc123",
    "value": 5999
  },
  {
    "id": 2,
    "UserDocument": "987654321",
    "CreditCardToken": "xyz456",
    "value": 1000
  },
  {
    "id": 3,
    "UserDocument": "876543210",
    "CreditCardToken": "789xpt0",
    "value": 1500
  }
]
```

### 2. Obter dados sensíveis por ID

```http
GET /api/SensitiveData/{id}
```

#### Resposta de Sucesso

```json
{
  "id": 1,
  "UserDocument": "123456789",
  "CreditCardToken": "abc123",
  "value": 5999
}
```

### 3. Criar dados sensíveis

```http
POST /api/SensitiveData
```

#### Corpo da Requisição

```json
{
  "UserDocument": "123456789",
  "CreditCardToken": "abc123",
  "value": 2000
}
```

#### Resposta de Sucesso

```json
{
  "id": 4,
  "UserDocument": "123456789",
  "CreditCardToken": "abc123",
  "value": 2000
}
```

### 4. Atualizar dados sensíveis por ID

```http
PUT /api/SensitiveData/{id}
```

#### Corpo da Requisição

```json
{
  "UserDocument": "987654321",
  "CreditCardToken": "xyz456",
  "value": 3000
}
```

#### Resposta de Sucesso

```json
{
  "id": 2,
  "UserDocument": "987654321",
  "CreditCardToken": "xyz456",
  "value": 3000
}
```

### 5. Excluir dados sensíveis por ID

```http
DELETE /api/SensitiveData/{id}
```

#### Resposta de Sucesso

```json
{
  "id": 3,
  "UserDocument": "876543210",
  "CreditCardToken": "789xpt0",
  "value": 1500
}
```

## Configurações de Criptografia

A criptografia na API utiliza o algoritmo AES com a chave e IV configurados. As configurações podem ser ajustadas no arquivo de configuração da aplicação.

```json
{
  "Encrypt": {
    "Key": "YourEncryptionKeyHere",
    "Iv": "YourEncryptionIvHere"
  }
}
```