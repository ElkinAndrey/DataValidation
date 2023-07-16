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


Если checkHasBeenStarted равно false, то вернуться только не проверенные данные. Если равен true, то вернуться 
только проверенные данные. Если null, то вернуться все данные. 
Также есть параметр isValid. Если он равер false, то вернуться данные, которые не прошли проверку. Если true, 
то только данные, прошедшие проверку. Если null, то только данные, находящиеся на проверке, но еще не 
проверенные.


Вернуть все данные
Вернуть только данные, которые не на провереке
Вернуть все данные на провереке
Вернуть только данные, прошедшие проверку
Вернуть только данные, не прошедшие проверку
Вернуть только данные, у которых только начали проверку