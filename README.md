"# credit-calculator" 

Установка утилит для генрации хеша пароля
sudo apt-get update && sudo apt-get install apache2-utils

Создание pfx из pem файлов

openssl pkcs12 -export -in /fullchain.pem -inkey /privkey.pem -out sanqit.ru.pfx
создать зашифрованый key файл из pfx
openssl pkcs12 -in [certificate.pfx] -nocerts -out [keyfile-encrypted.key]
создать расшифрованный key файл из pfx
openssl rsa -in [keyfile-encrypted.key] -out [keyfile-decrypted.key]
получить cer из pfx
openssl pkcs12 -in [certificate.pfx] -clcerts -nokeys -out [certificate.crt]