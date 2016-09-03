Ext.define('WC.model.Client', {
    extend: 'WC.model.BaseModel',

    fields: [
        { name: 'Id',  type: 'int' },
        { name: 'Active', type: 'bool' },
        { name: 'CorporateName', type: 'string' },
        { name: 'ComercialName', type: 'string' },
        { name: 'Email', type: 'string' },
        { name: 'Language',type: 'string' },
        { name: 'Website', type: 'string' },
        { name: 'Observations', type: 'string' },
        { name: 'Birthday', type: 'date' },
        { name: 'Website', type: 'string' },
        { name: 'SectorId',type: 'int' },
        { name: 'ZoneId', type: 'int' },
        { name: 'ChargeMethodId', type: 'int' },
        { name: 'EnableCurrentAccount', type: 'bool' },
        { name: 'CientTypeId', type: 'int' },
        { name: 'PaymentDueDateTypeId', type: 'int' },
        { name: 'PaymentDay', type: 'date' },
        { name: 'ClientOrderId', type: 'int' },
    ],
    idProperty: 'id'

});

<Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
<Property Name="Active" Type="bit" Nullable="false" />
<Property Name="ClientCode" Type="int" Nullable="false" />
<Property Name="CorporateName" Type="nvarchar(max)" Nullable="false" />
<Property Name="ComercialName" Type="nvarchar(max)" Nullable="false" />
<Property Name="Email" Type="nvarchar(max)" Nullable="false" />
<Property Name="Language" Type="nvarchar(max)" Nullable="false" />
<Property Name="WebSite" Type="nvarchar(max)" Nullable="false" />
<Property Name="Observations" Type="nvarchar(max)" Nullable="false" />
<Property Name="BirthDate" Type="datetime" Nullable="false" />
<Property Name="SectorId" Type="int" />
<Property Name="ZoneId" Type="int" />
<Property Name="ChargeMethodId" Type="int" Nullable="false" />
<Property Name="EnableCurrentAccount" Type="bit" Nullable="false" />
<Property Name="CientTypeId" Type="int" Nullable="false" />
<Property Name="PaymentDueDateTypeId" Type="int" />
<Property Name="PaymentDay" Type="time" Precision="7" Nullable="false" />
<Property Name="ClientOrderId" Type="int" Nullable="false" />
