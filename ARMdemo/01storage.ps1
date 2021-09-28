Import-Module Az.Resources
$rg = 'demo-rg-arm-practice'
New-AzResourceGroup -Name $rg -Location eastus -Force

New-AzResourceGroupDeployment `
    -ResourceGroupName $rg `
    -TemplateFile "01storage.json" `
    -TemplateParameterFile "01storage.parameters.json"
    #-TemplateFile "01storage.bicep"

#New-AzResourceGroupDeployment `
#    -ResourceGroupName $rg 
#    -Mode complete `
#    -TemplateFile "clearup.json"