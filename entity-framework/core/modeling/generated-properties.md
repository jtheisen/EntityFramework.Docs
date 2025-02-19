---
title: Generated Values - EF Core
author: rowanmiller
ms.date: 10/27/2016
ms.assetid: eb082011-11a1-41b4-a108-15daafa03e80
uid: core/modeling/generated-properties
---

# Generated Values

## Value Generation Patterns

There are three value generation patterns that can be used for properties:

* No value generation
* Value generated on add
* Value generated on add or update

### No value generation

No value generation means that you will always supply a valid value to be saved to the database. This valid value must be assigned to new entities before they are added to the context.

### Value generated on add

Value generated on add means that a value is generated for new entities.

Depending on the database provider being used, values may be generated client side by EF or in the database. If the value is generated by the database, then EF may assign a temporary value when you add the entity to the context. This temporary value will then be replaced by the database generated value during `SaveChanges()`.

If you add an entity to the context that has a value assigned to the property, then EF will attempt to insert that value rather than generating a new one. A property is considered to have a value assigned if it is not assigned the CLR default value (`null` for `string`, `0` for `int`, `Guid.Empty` for `Guid`, etc.). For more information, see [Explicit values for generated properties](../saving/explicit-values-generated-properties.md).

> [!WARNING]  
> How the value is generated for added entities will depend on the database provider being used. Database providers may automatically setup value generation for some property types, but others may require you to manually setup how the value is generated.
>
> For example, when using SQL Server, values will be automatically generated for `GUID` properties (using the SQL Server sequential GUID algorithm). However, if you specify that a `DateTime` property is generated on add, then you must setup a way for the values to be generated. One way to do this, is to configure a default value of `GETDATE()`, see [Default Values](relational/default-values.md).

### Value generated on add or update

Value generated on add or update means that a new value is generated every time the record is saved (insert or update).

Like `value generated on add`, if you specify a value for the property on a newly added instance of an entity, that value will be inserted rather than a value being generated. It is also possible to set an explicit value when updating. For more information, see [Explicit values for generated properties](../saving/explicit-values-generated-properties.md).

> [!WARNING]
> How the value is generated for added and updated entities will depend on the database provider being used. Database providers may automatically setup value generation for some property types, while others will require you to manually setup how the value is generated.
>
> For example, when using SQL Server, `byte[]` properties that are set as generated on add or update and marked as concurrency tokens, will be setup with the `rowversion` data type - so that values will be generated in the database. However, if you specify that a `DateTime` property is generated on add or update, then you must setup a way for the values to be generated. One way to do this, is to configure a default value of `GETDATE()` (see [Default Values](relational/default-values.md)) to generate values for new rows. You could then use a database trigger to generate values during updates (such as the following example trigger).
>
> [!code-sql[Main](../../../samples/core/Modeling/FluentAPI/ValueGeneratedOnAddOrUpdate.sql)]

## Conventions

By convention, non-composite primary keys of type short, int, long, or Guid will be setup to have values generated on add. All other properties will be setup with no value generation.

## Data Annotations

### No value generation (Data Annotations)

[!code-csharp[Main](../../../samples/core/Modeling/DataAnnotations/ValueGeneratedNever.cs#Sample)]

### Value generated on add (Data Annotations)

[!code-csharp[Main](../../../samples/core/Modeling/DataAnnotations/ValueGeneratedOnAdd.cs#Sample)]

> [!WARNING]  
> This just lets EF know that values are generated for added entities, it does not guarantee that EF will setup the actual mechanism to generate values. See [Value generated on add](#value-generated-on-add) section for more details.

### Value generated on add or update (Data Annotations)

[!code-csharp[Main](../../../samples/core/Modeling/DataAnnotations/ValueGeneratedOnAddOrUpdate.cs#Sample)]

> [!WARNING]  
> This just lets EF know that values are generated for added or updated entities, it does not guarantee that EF will setup the actual mechanism to generate values. See [Value generated on add or update](#value-generated-on-add-or-update) section for more details.

## Fluent API

You can use the Fluent API to change the value generation pattern for a given property.

### No value generation (Fluent API)

[!code-csharp[Main](../../../samples/core/Modeling/FluentAPI/ValueGeneratedNever.cs#Sample)]

### Value generated on add (Fluent API)

[!code-csharp[Main](../../../samples/core/Modeling/FluentAPI/ValueGeneratedOnAdd.cs#Sample)]

> [!WARNING]  
> `ValueGeneratedOnAdd()` just lets EF know that values are generated for added entities, it does not guarantee that EF will setup the actual mechanism to generate values.  See [Value generated on add](#value-generated-on-add) section for more details.

### Value generated on add or update (Fluent API)

[!code-csharp[Main](../../../samples/core/Modeling/FluentAPI/ValueGeneratedOnAddOrUpdate.cs#Sample)]

> [!WARNING]  
> This just lets EF know that values are generated for added or updated entities, it does not guarantee that EF will setup the actual mechanism to generate values. See [Value generated on add or update](#value-generated-on-add-or-update) section for more details.
