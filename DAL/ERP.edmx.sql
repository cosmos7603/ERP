
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/27/2016 15:00:56
-- Generated from EDMX file: C:\DevelopmentGit\dynamika-app\ERP\DAL\ERP.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ERP];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClientOrderChargeMethod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrders] DROP CONSTRAINT [FK_ClientOrderChargeMethod];
GO
IF OBJECT_ID(N'[dbo].[FK_ComisionTypeComercialAgent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ComercialAgents] DROP CONSTRAINT [FK_ComisionTypeComercialAgent];
GO
IF OBJECT_ID(N'[dbo].[FK_ProviderProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProviderProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductFamilyProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductFamilyProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_BillDetailProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillDetails] DROP CONSTRAINT [FK_BillDetailProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_SaleBillDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillDetails] DROP CONSTRAINT [FK_SaleBillDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_SaleSaleState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sales] DROP CONSTRAINT [FK_SaleSaleState];
GO
IF OBJECT_ID(N'[dbo].[FK_SaleSaleCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sales] DROP CONSTRAINT [FK_SaleSaleCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_BillTypeSale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sales] DROP CONSTRAINT [FK_BillTypeSale];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientTypeClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clients] DROP CONSTRAINT [FK_ClientTypeClient];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientSale2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sales] DROP CONSTRAINT [FK_ClientSale2];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ChargeMethods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChargeMethods];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[ClientTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientTypes];
GO
IF OBJECT_ID(N'[dbo].[ComercialAgents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ComercialAgents];
GO
IF OBJECT_ID(N'[dbo].[CustomerOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerOrders];
GO
IF OBJECT_ID(N'[dbo].[PaymentDueDateTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentDueDateTypes];
GO
IF OBJECT_ID(N'[dbo].[ProductFamilies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductFamilies];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Providers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Providers];
GO
IF OBJECT_ID(N'[dbo].[Taxes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Taxes];
GO
IF OBJECT_ID(N'[dbo].[ComisionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ComisionTypes];
GO
IF OBJECT_ID(N'[dbo].[Sales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sales];
GO
IF OBJECT_ID(N'[dbo].[SaleCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleCategories];
GO
IF OBJECT_ID(N'[dbo].[BillDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillDetails];
GO
IF OBJECT_ID(N'[dbo].[SaleStates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleStates];
GO
IF OBJECT_ID(N'[dbo].[BillTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ChargeMethods'
CREATE TABLE [dbo].[ChargeMethods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ComercialName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [Observations] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [Address1] nvarchar(max)  NULL,
    [Address2] nvarchar(max)  NULL,
    [Province] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [ZipCode] nvarchar(max)  NULL,
    [Telephone1] nvarchar(max)  NULL,
    [Thelephone2] nvarchar(max)  NULL,
    [CUIT] nvarchar(max)  NOT NULL,
    [ClientTypeId] int  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [DNI] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ClientTypes'
CREATE TABLE [dbo].[ClientTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ComercialAgents'
CREATE TABLE [dbo].[ComercialAgents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ComercialAgentCode] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [Address1] nvarchar(max)  NULL,
    [Address2] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [Province] nvarchar(max)  NULL,
    [ZipCode] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Telephone] nvarchar(max)  NULL,
    [Cellphone] nvarchar(max)  NULL,
    [StartDate] nvarchar(max)  NULL,
    [Observations] nvarchar(max)  NULL,
    [ComisionAmount] nvarchar(max)  NOT NULL,
    [ComisionTypeId] int  NULL
);
GO

-- Creating table 'CustomerOrders'
CREATE TABLE [dbo].[CustomerOrders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [DeliveryDate] datetime  NOT NULL,
    [OrderCode] nvarchar(max)  NOT NULL,
    [Reference] nvarchar(max)  NOT NULL,
    [ChargeMethodId] int  NOT NULL,
    [PaymentDueDateTypeId] int  NOT NULL,
    [DeliveryAddress] nvarchar(max)  NOT NULL,
    [ComercialAgent] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PaymentDueDateTypes'
CREATE TABLE [dbo].[PaymentDueDateTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductFamilies'
CREATE TABLE [dbo].[ProductFamilies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductCode] nvarchar(max)  NOT NULL,
    [ShortDescription] nvarchar(max)  NOT NULL,
    [LongDescription] nvarchar(max)  NOT NULL,
    [Active] bit  NULL,
    [AvailableForSale] bit  NULL,
    [SalePrice] decimal(18,0)  NOT NULL,
    [Cost] decimal(18,0)  NOT NULL,
    [Stock] int  NOT NULL,
    [ProviderId] int  NULL,
    [ProductFamilyId] int  NULL
);
GO

-- Creating table 'Providers'
CREATE TABLE [dbo].[Providers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Country] nvarchar(max)  NULL,
    [Address1] nvarchar(max)  NULL,
    [Address2] nvarchar(max)  NULL,
    [Province] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [ZipCode] nvarchar(max)  NULL,
    [Telephone2] nvarchar(max)  NULL,
    [Telephone1] nvarchar(max)  NULL,
    [ComercialName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [Observations] nvarchar(max)  NULL,
    [CUIT] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Taxes'
CREATE TABLE [dbo].[Taxes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ComisionTypes'
CREATE TABLE [dbo].[ComisionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sales'
CREATE TABLE [dbo].[Sales] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmisionDate] datetime  NOT NULL,
    [DueDate] datetime  NOT NULL,
    [BillNumber] nvarchar(max)  NOT NULL,
    [TotalAmount] decimal(18,0)  NOT NULL,
    [AmountToCharge] decimal(18,0)  NOT NULL,
    [SaleStateId] int  NOT NULL,
    [SaleCategoryId] int  NOT NULL,
    [BillTypeId] int  NOT NULL,
    [Observations] nvarchar(max)  NULL,
    [ClientId] int  NOT NULL
);
GO

-- Creating table 'SaleCategories'
CREATE TABLE [dbo].[SaleCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BillDetails'
CREATE TABLE [dbo].[BillDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] nvarchar(max)  NOT NULL,
    [SaleId] int  NOT NULL,
    [Product_Id] int  NOT NULL
);
GO

-- Creating table 'SaleStates'
CREATE TABLE [dbo].[SaleStates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BillTypes'
CREATE TABLE [dbo].[BillTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ChargeMethods'
ALTER TABLE [dbo].[ChargeMethods]
ADD CONSTRAINT [PK_ChargeMethods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientTypes'
ALTER TABLE [dbo].[ClientTypes]
ADD CONSTRAINT [PK_ClientTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ComercialAgents'
ALTER TABLE [dbo].[ComercialAgents]
ADD CONSTRAINT [PK_ComercialAgents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrders'
ALTER TABLE [dbo].[CustomerOrders]
ADD CONSTRAINT [PK_CustomerOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentDueDateTypes'
ALTER TABLE [dbo].[PaymentDueDateTypes]
ADD CONSTRAINT [PK_PaymentDueDateTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductFamilies'
ALTER TABLE [dbo].[ProductFamilies]
ADD CONSTRAINT [PK_ProductFamilies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Providers'
ALTER TABLE [dbo].[Providers]
ADD CONSTRAINT [PK_Providers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Taxes'
ALTER TABLE [dbo].[Taxes]
ADD CONSTRAINT [PK_Taxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ComisionTypes'
ALTER TABLE [dbo].[ComisionTypes]
ADD CONSTRAINT [PK_ComisionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [PK_Sales]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SaleCategories'
ALTER TABLE [dbo].[SaleCategories]
ADD CONSTRAINT [PK_SaleCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BillDetails'
ALTER TABLE [dbo].[BillDetails]
ADD CONSTRAINT [PK_BillDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SaleStates'
ALTER TABLE [dbo].[SaleStates]
ADD CONSTRAINT [PK_SaleStates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BillTypes'
ALTER TABLE [dbo].[BillTypes]
ADD CONSTRAINT [PK_BillTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ChargeMethodId] in table 'CustomerOrders'
ALTER TABLE [dbo].[CustomerOrders]
ADD CONSTRAINT [FK_ClientOrderChargeMethod]
    FOREIGN KEY ([ChargeMethodId])
    REFERENCES [dbo].[ChargeMethods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientOrderChargeMethod'
CREATE INDEX [IX_FK_ClientOrderChargeMethod]
ON [dbo].[CustomerOrders]
    ([ChargeMethodId]);
GO

-- Creating foreign key on [ComisionTypeId] in table 'ComercialAgents'
ALTER TABLE [dbo].[ComercialAgents]
ADD CONSTRAINT [FK_ComisionTypeComercialAgent]
    FOREIGN KEY ([ComisionTypeId])
    REFERENCES [dbo].[ComisionTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ComisionTypeComercialAgent'
CREATE INDEX [IX_FK_ComisionTypeComercialAgent]
ON [dbo].[ComercialAgents]
    ([ComisionTypeId]);
GO

-- Creating foreign key on [ProviderId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProviderProduct]
    FOREIGN KEY ([ProviderId])
    REFERENCES [dbo].[Providers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProviderProduct'
CREATE INDEX [IX_FK_ProviderProduct]
ON [dbo].[Products]
    ([ProviderId]);
GO

-- Creating foreign key on [ProductFamilyId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductFamilyProduct]
    FOREIGN KEY ([ProductFamilyId])
    REFERENCES [dbo].[ProductFamilies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductFamilyProduct'
CREATE INDEX [IX_FK_ProductFamilyProduct]
ON [dbo].[Products]
    ([ProductFamilyId]);
GO

-- Creating foreign key on [Product_Id] in table 'BillDetails'
ALTER TABLE [dbo].[BillDetails]
ADD CONSTRAINT [FK_BillDetailProduct]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillDetailProduct'
CREATE INDEX [IX_FK_BillDetailProduct]
ON [dbo].[BillDetails]
    ([Product_Id]);
GO

-- Creating foreign key on [SaleId] in table 'BillDetails'
ALTER TABLE [dbo].[BillDetails]
ADD CONSTRAINT [FK_SaleBillDetail]
    FOREIGN KEY ([SaleId])
    REFERENCES [dbo].[Sales]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SaleBillDetail'
CREATE INDEX [IX_FK_SaleBillDetail]
ON [dbo].[BillDetails]
    ([SaleId]);
GO

-- Creating foreign key on [SaleStateId] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_SaleSaleState]
    FOREIGN KEY ([SaleStateId])
    REFERENCES [dbo].[SaleStates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SaleSaleState'
CREATE INDEX [IX_FK_SaleSaleState]
ON [dbo].[Sales]
    ([SaleStateId]);
GO

-- Creating foreign key on [SaleCategoryId] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_SaleSaleCategory]
    FOREIGN KEY ([SaleCategoryId])
    REFERENCES [dbo].[SaleCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SaleSaleCategory'
CREATE INDEX [IX_FK_SaleSaleCategory]
ON [dbo].[Sales]
    ([SaleCategoryId]);
GO

-- Creating foreign key on [BillTypeId] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_BillTypeSale]
    FOREIGN KEY ([BillTypeId])
    REFERENCES [dbo].[BillTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillTypeSale'
CREATE INDEX [IX_FK_BillTypeSale]
ON [dbo].[Sales]
    ([BillTypeId]);
GO

-- Creating foreign key on [ClientTypeId] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [FK_ClientTypeClient]
    FOREIGN KEY ([ClientTypeId])
    REFERENCES [dbo].[ClientTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientTypeClient'
CREATE INDEX [IX_FK_ClientTypeClient]
ON [dbo].[Clients]
    ([ClientTypeId]);
GO

-- Creating foreign key on [ClientId] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_ClientSale2]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientSale2'
CREATE INDEX [IX_FK_ClientSale2]
ON [dbo].[Sales]
    ([ClientId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------