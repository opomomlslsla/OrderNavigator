Приложение которое фильтрует заказы (Orders) по заданным параметрам, а именно District - район и временные метки от и до (start и end).
Логгирование сделано с помощью библиотеки Serilog.
Для обработки ошибок используется самописный middleware "ExceptionHandler".
для валидации использована библиотека FluentValidation.
Класс DataSeeder добавляет в бд начальные данные небольшое кол-во заказов, чтобы можно было сразу проверить работу приложения.

Инструкция по конфигурации:

в файле appsettingsjson можно редактировать куда сохранять результаты фильтровки.
![изображение](https://github.com/user-attachments/assets/3f9e8691-2f30-480c-80bf-b1dd9ebbb10f)
<br>
Если поставить такие настройки то в файл, путь к которому нужно указать в FilePath, создастся txt файл с результатом фильтрации.
Внимание! следует использовать двойной слеш чтобы путь к файлу был валидным.
также результат получает клиент который сделал запрос.
В appsettingsjson можно указать параметры для логгирования (по умолчанию логгировние происходит в бд в таблицу Logs).

Инструкция по запуску:
Перед первым запуском программы следует законментировать строки:<br>
builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
затем создать базу данных с помощью команды update-database в консоли диспетчера пакетов
<br>
![изображение](https://github.com/user-attachments/assets/563df558-f162"-4656-844a-6788ebe97883)
<br>
поставив в проект по умолчанию Infrastructure <br>
![изображение](https://github.com/user-attachments/assets/29878cb6-dbd3-43e5-94a0-d6cc6e220354)
<br>
после можно все раскоментировать и запускать приложение.

Для того чтобы запустить проект с помощью .net CLI вам нужно установить .net sdk(если у вас его нет), найти .net sdk можно на официальном сайте майкрософт. 
https://dotnet.microsoft.com/en-us/download - ссылка на скачивание .net sdk.
после установки вам нужно открыть командную строку и набрать следующие команды:<br>
cd <#ваш путь к папке с проектом> <br>
dotnet build <br>
dotnet run <br>

Для того чтобы запустить программу в visual studio вам нужно открыть решение в visual studio и запустить отладку.
