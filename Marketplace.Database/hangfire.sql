-- IF DB_ID('Hangfire') IS NULL
BEGIN
    CREATE DATABASE [Hangfire]
END
GO

-- IF DB_ID('MarketplaceDev') IS NULL
BEGIN
    CREATE DATABASE [MarketplaceDev]
END
GO
