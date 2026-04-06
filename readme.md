testing branch
az eventhubs eventhub create --name pet-events --namespace-name pet-eh-namespace --resource-group ant-aks-demo
az eventhubs namespace authorization-rule keys list --resource-group ant-aks-demo --namespace-name pet-eh-namespace --name RootManageSharedAccessKey


