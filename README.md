<a id="ser"></a>

# Сервис для проверки данных

На сервисе есть пользователи, менеджеры и администраторы. Пользователи оставляют данные на проверку. Также пользователи могут видеть только проверенные данные. Менеджеры могут проверять и видеть любые данные. Администраторы могут делать что угодно.

<a id="sercontent"></a>

## Оглавление

* [Сервис для проверки данных](#ser)
    * [Содержание](#sercontent)
* [API для сервиса проверки данных](#api)
    * [О проекте](#apiabout)
    * [Проекты в решении и установленные пакеты](#apipackag)
    * [Настройка окружения разработки для Windows](#apisettings)
    * [Как использовать приложение](#apiusing)
    * [Добавление миграций и создание базы данных](#apimigrations)



<a id="api"></a>

# API для сервиса проверки данных

REST API сервиса. Получает и отправляет на сервер JSON с данными. 

<a id="apiabout"></a>

## О проекте

API содержит 34 конечные точки. В приложении есть возможность авторизации.

Платформа: .NET 7.0    
Язык программирования: C# 11  
Фреймворк для создания серверной части: ASP.NET Core 7.0.9  
Фреймворк для работы с базой данных: Entity Framework Core 7.0.7  
База данных: MS SQL Server  
IDE: Visual Studio Community 2022
ОС: Windows 11 Pro Версия 22H2

<a id="apipackag"></a>

## Проекты в решении и установленные пакеты

* DataValidationAPI.Domain - проект с сущностями и константами. Шаблон проекта "Библиотека классов (Майкрософт)".
* DataValidationAPI.Persistence - проект с репозиториями, контекстом базы данных и миграциями. Шаблон проекта "Библиотека классов (Майкрософт)".
    * Microsoft.EntityFrameworkCore.SqlServer 7.0.7 - для создания базы данных в MS SQL Server
* DataValidationAPI.Service - провект с сервисами. Шаблон проекта "Библиотека классов (Майкрософт)".
    * Microsoft.AspNetCore.Cryptography.KeyDerivation 7.0.9 - для хэширования пароля
* DataValidationAPI.Infrastructure  проекс с DTO и мидлвейр. Шаблон проекта "Библиотека классов (Майкрософт)".
    * Microsoft.AspNetCore.Http.Abstractions 2.2.0 - для создания своих Middlewares
* DataValidationAPI.Presentation - проект с контроллерами. Шаблон проекта "Веб-API ASP.NET Core (Майкрософт)".
    * Microsoft.AspNetCore.Authentication.JwtBearer 7.0.9 - для добавления JWT токенов  
    * Microsoft.AspNetCore.Mvc.NewtonsoftJson 7.0.9 - для того, чтобы не было циклических сериализаций в JSON  
    * Microsoft.AspNetCore.OpenApi 7.0.8 - для работы с ASP.NET Core  
    * Microsoft.EntityFrameworkCore.Design 7.0.7 - для создания миграций базы данных  
    * Swashbuckle.AspNetCore 6.5.0 - для возможности работы со Swagger  

<a id="apisettings"></a>

## Настройка окружения разработки для Windows

### Скачивание проекта

1. Зайдите в проект на GitHub ([Проект](https://github.com/ElkinAndrey/DataValidation))  
2. Нажмите на кнопку "Code"  
3. Нажмите на кнопку "Download ZIP"  
&nbsp;<img src="images\API\Download1.png" width="400px">  
4. Распакуйте скачанный файл

### Установка Visual Studio

1. Скачайте Visual Studio 2022 Community с официального сайта (VS 2022 или выше) ([Скачать](https://visualstudio.microsoft.com/ru/downloads/))
2. Запустите скачанный установочный файл
3. Нажимайте "продолжить", пока не появится окно установки.
4. В окне установки поставьте галочки у нужных инструмента "ASP.NET и разработка веб-приложений".
&nbsp;<img src="images\API\VS1.png" width="400px">  
5. Нажмите кнопку "Установить".  

### Создание базы данных

**Ремарка**  
Для смены диска в командной строке введите команду "D:", где D имя диска.  
Для перехода по папкам в командной строке введите команду "cd D:\DataValidation", где D:\DataValidation путь к папке.  
Для просмотра папок и файлов нажимайте Tab, так в консоли будут пролистываться папки и файлы.   

1. Откройте "Командную строку"  
1.1. Нажмите сочетание клавиш "Win + R"  
1.2. В открывшемся окне вбиваете "cmd"  
1.3. Нажимаете кнопку "Ок"  
2. Откройте папку "DataValidation" со скачанным проектом. Например, у меня этот путь "C:\Users\123\Desktop\DataValidation-main".
3. В консоли от папки с проектом перейдите в папку "DataValidationAPI". В ней лежат папки "DataValidationAPI.Domain", "DataValidationAPI.Infrastructure" и другие.
4. В командной строке введите "dotnet tool install --global dotnet-ef --version 7.*" для установки средств Entity Framework
5. После установки в командной строке введите "dotnet ef database update --project .\DataValidationAPI.Persistence\DataValidationAPI.Persistence.csproj --startup-project .\DataValidationAPI.Presentation\DataValidationAPI.Presentation.csproj" для создания базы данных проекта

### Запуск проекта

1. Откройте папку "DataValidation" со скачанным проектом.
2. В папке должен лежать файл "DataValidation.sln". Откройте этот файл при помощи Visual Studio.
&nbsp;<img src="images\API\VS2.png" width="400px">  
3. После открытия проекта в Visual Studio сверху нажмите на кнопку запуска проекта

<a id="apiusing"></a>

## Как использовать приложение

После запуска приложения, откроется браузер, где появится окно Swagger с конечными точками.  
&nbsp;<img src="images\API\Swagger1.png">  
Изначально база данных уже будет заполнена данными. В в ней будут лежать следующие аккаунты: 

* Email: 1, Пароль: 1 - Пользователь (аккаунт по умолчанию заблокирован)
* Email: 2, Пароль: 2 - Пользователь
* Email: 3, Пароль: 3 - Менеджер
* Email: 4, Пароль: 4 - Менеджер
* Email: 5, Пароль: 5 - Администратор
* Email: 6, Пароль: 6 - Администратор

Теперь необходимо проверить работоспособность
1. Для входа в аккаунт выберите конечную точку "/api/auth/login".  
2. В раскрывшемся меню нажмите на кнопку "Try it out".  
&nbsp;<img src="images\API\Swagger2.png" width="400px">  
3. В теле запроса укажите Email и пароль из списка.  
4. Нажмите кнопку "Execute"  
5. Если все было проделано верно, то программа вернет JWT токен (например, eyJhbGciOiJodHRwOi8vd3d...). Токен находится в разделе "Code". Скопируйте токен.  
&nbsp;<img src="images\API\Swagger3.png" width="400px">  
6. В правом верхнем углу нажммите зеленую кнопку "Authorize".  
7. В открывшемся окне введите параметр "Value". Необходимо ввести слово bearer и затем скопированный токен (например, bearer eyJhbGciOiJodHRwOi8vd3d...)  
8. Нажмите кнопку "Authorize"  
&nbsp;<img src="images\API\Swagger4.png" width="400px">  

Теперь можно вызывать конечные точки. Некоторые точки доступны только менеджерам и администраторам. Чтобы протестировать все функции войдите в аккаунт администратора. Конечные точки тестируются аналогично входу в аккаунт. То есть в формате JSON вводятся необходимые параметы, по которым вернуться нужные данные. 

<a id="apimigrations"></a>

## Добавление миграций и создание базы данных

Если появится необходимость разрабатывать проект дальше, то скорее всего придется обновлять миграции. При помощи них создается база данных. Контекст базы данных и запускаемый проект находятся в разных проектах, поэтому необходимо явно указывать, где находится контекст, а где запускаемый проект.

Зайдите в папку "DataValidationAPI", где находятся проекты "DataValidationAPI.Presentation" и "DataValidationAPI.Persistence". Введите команды для добавления миграций и для создания базы данных.

### Команда для добавления миграциий

<pre>
dotnet ef migrations add Initial --project .\DataValidationAPI.Persistence\DataValidationAPI.Persistence.csproj --startup-project .\DataValidationAPI.Presentation\DataValidationAPI.Presentation.csproj
</pre>

### Команда для создания базы данных

<pre>
dotnet ef database update --project .\DataValidationAPI.Persistence\DataValidationAPI.Persistence.csproj --startup-project .\DataValidationAPI.Presentation\DataValidationAPI.Presentation.csproj
</pre>