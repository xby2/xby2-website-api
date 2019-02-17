provider "azurerm" {
  version = "=1.22.0"
}

resource "azurerm_resource_group" "xby2_website_rg" {
  name     = "xby2-website-prod-ncus-rg"
  location = "North Central US"
}

resource "azurerm_app_service_plan" "xby2_website_asp" {
  name                = "xby2-website-prod-ncus-asp"
  location            = "${azurerm_resource_group.xby2_website_rg.location}"
  resource_group_name = "${azurerm_resource_group.xby2_website_rg.name}"

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "xby2_website_api_as" {
  name                = "xby2-website-api-prod-ncus"
  location            = "${azurerm_resource_group.xby2_website_rg.location}"
  resource_group_name = "${azurerm_resource_group.xby2_website_rg.name}"
  app_service_plan_id = "${azurerm_app_service_plan.xby2_website_asp.id}"
  https_only          = true

  app_settings {
    "XBY2_WEBSITE_API_MAILJET_USERNAME" = ""
    "XBY2_WEBSITE_API_MAILJET_PASSWORD" = ""
  }

  // why doesn't site_config work?
}
