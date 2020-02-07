DELETE [dbo].[__MigrationHistory]
WHERE (([MigrationId] = N'202002070928288_CreatePriceLimit') AND ([ContextKey] = N'CarPool.Data.Migrations.Configuration'))
DROP TABLE [dbo].[PriceLimits]
DELETE [dbo].[__MigrationHistory]
WHERE (([MigrationId] = N'202002070919172_CreatePriceLimit') AND ([ContextKey] = N'CarPool.Data.Migrations.Configuration'))
