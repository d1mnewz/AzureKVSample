Azure Key Vault Setup
=====================

[[TOC]]

# Microsoft Docs
[Azure Key Vault](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-use-from-web-application)

# Generate certificate
[Download](https://developer.microsoft.com/en-us/windows/downloads/windows-10-sdk)

[MakeCert](https://msdn.microsoft.com/en-us/library/windows/desktop/aa386968(v=vs.85).aspx)

[Pvk2Pfx](https://docs.microsoft.com/en-us/windows-hardware/drivers/devtest/pvk2pfx)

```powershell
makecert -sv [cert_name].pvk -n "cn=[cert_name]" [cert_name].cer -e mm/dd/yyyy -len 2048
```
Type password 
```powershell
pvk2pfx -pvk [cert_name].pvk -spc [cert_name].cer -pfx [cert_name].pfx -po [password]
```

# Azure AD App
## Create Azure AD App with certificate
[Integrating applications with Azure Active Directory] (https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-integrating-applications)
Go to Application's Settings and add "Keys" with existing certificates.

# Create Azure Key Vault
[Watch](https://azure.microsoft.com/en-us/resources/videos/index/?services=key-vault)
[Secure your key vault](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-secure-your-key-vault)
Add Application to Azure Key Vault and set corresponding rights

# Configuration
## On machine 
Just Install <b>.pfx</b> on machine using password
## On App Service
1) Go to App
2) "SSL Sertificates"
3) Upload certificate and select <b>.pfx</b> enter password and click "Upload"
4) Add "Application Setting"  <b>WEBSITE_LOAD_CERTIFICATES</b> with value <b>*</b>