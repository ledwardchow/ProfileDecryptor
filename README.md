ProfileDecryptor
================

Decrypt EncryptedPayloadContent blocks in iOS MobileConfig files with a locally-installed private key.

1. Install the p12 certificate that contains the decryption private key into your Windows certificate store
2. Paste the base64-encoded text in the EncryptedPayloadContent section, within the <data> tags into the big textbox
3. Click "Decrypt". The decrypted payload will be saved to c:\temp\decrypted.txt.
