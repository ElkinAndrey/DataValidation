## Добавление миграций и создание базы данных

База данных создается при помощи миграциий. Контекст базы данных и запускаемый проект находятся в разных проектах, поэтому необходимо явно указывать, где находится контекст, а где запускаемый проект.

Зайдите в папку "DataValidationAPI", где находятся проекты "DataValidationAPI.Presentation" и "DataValidationAPI.Persistence". Введите команды для добавления миграций и для создания базы данных.

### Команда для добавления миграциий

<pre>
dotnet ef migrations add Initial --project .\DataValidationAPI.Persistence\DataValidationAPI.Persistence.csproj --startup-project .\DataValidationAPI.Presentation\DataValidationAPI.Presentation.csproj
</pre>

### Команда для создания базы данных

<pre>
dotnet ef database update --project .\DataValidationAPI.Persistence\DataValidationAPI.Persistence.csproj --startup-project .\DataValidationAPI.Presentation\DataValidationAPI.Presentation.csproj
</pre>
