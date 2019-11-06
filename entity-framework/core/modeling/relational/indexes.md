---
title: Indexes (Relational Database) - EF Core
author: AndriySvyryd
ms.author: ansvyryd
ms.date: 11/05/2016
uid: core/modeling/relational/indexes
---
# Indexes (Relational Database)

> [!NOTE]  
> The configuration in this section is applicable to relational databases in general. The extension methods shown here will become available when you install a relational database provider (due to the shared *Microsoft.EntityFrameworkCore.Relational* package).

An index in a relational database maps to the same concept as an index in the core of Entity Framework.

## Conventions

By convention, indexes are named `IX_<type name>_<property name>`. For composite indexes `<property name>` becomes an underscore separated list of property names.

## Data Annotations

Indexes can not be configured using Data Annotations.

## Fluent API

You can use the Fluent API to configure the name of an index.

[!code-csharp[Main](../../../../samples/core/Modeling/FluentAPI/Relational/IndexName.cs?name=Model&highlight=9)]

You can also specify a filter.

[!code-csharp[Main](../../../../samples/core/Modeling/FluentAPI/Relational/IndexFilter.cs?name=Model&highlight=9)]

When using the SQL Server provider EF adds a `'IS NOT NULL'` filter for all nullable columns that are part of a unique index. To override this convention you can supply a `null` value.

[!code-csharp[Main](../../../../samples/core/Modeling/FluentAPI/Relational/IndexNoFilter.cs?name=Model&highlight=10)]

### Include Columns in SQL Server Indexes

You can configure [indexes with included columns](https://docs.microsoft.com/sql/relational-databases/indexes/create-indexes-with-included-columns) to significantly improve query performance when all columns in the query are included in the index as key or non-key columns.

[!code-csharp[Main](../../../../samples/core/Modeling/FluentAPI/Relational/IndexInclude.cs?name=Model)]
