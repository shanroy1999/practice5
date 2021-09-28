@description('Unique DNS name of storage account')
@maxLength(30)
param storageName string

@description('Approved SKUs for deployment')
@allowed([
  'Standard_LRS'
  'Standard_GRS'
  'Standard_RAGRS'
  'Standard_ZRS'
  'Standard_GZRS'
  'Standard_RAGZRS'
])
param storageSKU string = 'Standard_GRS'

@description('Name of the App Service Plan used')
param planName string

@description('Name of the Web Application related to App Service Plan')
param webAppName string

resource armprcstorage01 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: 'armprcstorage01'
  tags: {
    displayName: 'armstorage01'
  }
  location: resourceGroup().location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_GRS'
  }
}

resource armprcstorage01_default_input1 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  name: 'armprcstorage01/default/input1'
  dependsOn: [
    armprcstorage01
  ]
}

resource armprcstorage01_default_output1 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  name: 'armprcstorage01/default/output1'
  dependsOn: [
    armprcstorage01
  ]
}

resource storageName_resource 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: storageName
  tags: {
    displayName: storageName
  }
  location: resourceGroup().location
  kind: 'StorageV2'
  sku: {
    name: storageSKU
  }
}

resource storageName_default_input2 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  name: '${storageName}/default/input2'
  dependsOn: [
    storageName_resource
  ]
}

resource storageName_default_output2 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  name: '${storageName}/default/output2'
  dependsOn: [
    storageName_resource
  ]
}

resource planName_resource 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: planName
  location: resourceGroup().location
  sku: {
    name: 'S1'
  }
  properties: {}
}

resource webAppName_resource 'Microsoft.Web/sites@2020-12-01' = {
  name: webAppName
  location: resourceGroup().location
  properties: {
    serverFarmId: planName_resource.id
    siteConfig: {
      appSettings: [
        {
          name: 'storageAccessKey'
          value: listKeys(storageName_resource.id, '2021-06-01').keys[0].value
        }
      ]
    }
  }
}
