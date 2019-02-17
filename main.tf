resource "azurerm_resource_group" "xby2_website_uat_rg" {
  name     = "xby2-website-uat-ncus-rg"
  location = "North Central US"
}

resource "azurerm_app_service_plan" "xby2_website_uat_asp" {
  name                = "xby2-website-uat-ncus-asp"
  location            = "${azurerm_resource_group.xby2_website_api_uat_rg.location}"
  resource_group_name = "${azurerm_resource_group.xby2_website_api_uat_rg.name}"

  sku {
    tier = "Free"
    size = "F"
  }
}

resource "azurerm_app_service" "xby2_website_api_uat_asp" {
  name                = "xby2-website-api-uat-ncus"
  location            = "${azurerm_resource_group.xby2_website_uat_rg.location}"
  resource_group_name = "${azurerm_resource_group.xby2_website_uat_rg.name}"
  app_service_plan_id = "${azurerm_app_service_plan.xby2_website_uat_asp.id}"
}
