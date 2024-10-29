Тестовое задание Effective Mobile.
Описание проекта:
<br>
Приложение которое фильтрует заказы (Orders) по заданным параметрам, а именно District - район и временные метки от и до (start и end).
Логгирование сделано с помощью библиотеки Serilog.
Для обработки ошибок используется самописный middleware "ExceptionHandler".
для валидации использована библиотека FluentValidation.
Класс DataSeeder добавляет в бд начальные данные небольшое кол-во заказов, чтобы можно было сразу проверить работу приложения.

Инструкция по конфигурации:

в файле appsettingsjson можно редактировать куда сохранять результаты фильтровки.
![изображение](https://github.com/user-attachments/assets/3f9e8691-2f30-480c-80bf-b1dd9ebbb10f)
<br>
Если поставить такие настройки то в файл, путь к которому нужно указать в FilePath, создастся txt файл с результатом фильтрации (если нет папки то будет ошибка поэтому нужно её создать). 
Внимание! следует использовать двойной слеш чтобы путь к файлу был валидным.
также результат получает клиент который сделал запрос.
В appsettingsjson можно указать параметры для логгирования (по умолчанию в txt файл по пути D:\Storage\Logs).

Инструкция по запуску: 
Перед запуском нужно создать базу данных для этого нужно открыть консоль диспетчера пакетов и внести команду-update-database иначе Dataseeder
(необязательный класс который я добавил чтобы можно было сразу проверить работу фильтрации выкинет ошибку)
выкинет ошибку что базы данных нет.
Для того чтобы запустить проект с помощью .net CLI вам нужно установить .net sdk(если у вас его нет), найти .net sdk можно на официальном сайте майкрософт. 
https://dotnet.microsoft.com/en-us/download - ссылка на скачивание .net sdk.
после установки вам нужно открыть командную строку и набрать следующие команды:<br>
cd <#ваш путь к папке с проектом> <br>
dotnet build <br>
dotnet run <br>

Для того чтобы запустить программу в visual studio вам нужно открыть решение в visual studio и запустить отладку.

<br>
Текст задания:
<br>
Необходимо разработать консольное приложение (по желанию можно WinForms
либо WebApi) для службы доставки, которое фильтрует заказы в зависимости от
количества обращений в конкретном районе города и времени обращения с и по.
Входные данные для каждой строки содержат следующую информацию:
● Номер заказа - можно использовать уникальный идентификатор или
придумать свой;
● Вес заказа в килограммах;
● Район заказа, можно придумать либо название либо идентификатор
района;
● Время доставки заказа - в формате: yyyy-MM-dd HH:mm:ss.
Для исходных данных можно использовать либо файл с данными
(рекомендуется) либо любые СУБД, которые можно легко установить для проверки.
Также необходимо сделать логирование основных операций, а также
валидацию входных данных.

В результирующий файл либо БД необходимо вывести результат фильтрации
заказов для доставки в конкретный район города в ближайшие полчаса после времени
первого заказа.
Данные для фильтрации (можно передавать через параметры консольного
приложения _cityDistrict, _firstDeliveryDateTime):
● Район доставки;
● Время первой доставки.
Выходные файлы:
● логирование - в случае консольного приложения определить задание адреса
файла через командную строку: _deliveryLog - путь к файлу с логами, либо
создать в СУБД таблицу с логами;
● результат - записывать по адресу: _deliveryOrder - путь к файлу с результатом
выборки либо в СУБД таблицу.
По возможности, кроме передачи параметров через командную строку либо
форму, можно реализовать частичную либо полную передачу параметров через файлы
конфигурации или переменные среды
Программа не должна ломаться от некорректных входных данных (реализовать
в валидации), ошибок ввода-вывода и прочим причинам, которые можно
предусмотреть (реализовать обработку возможных ошибок).
Решение должно быть предоставлено в виде исходных кодов в архиве, при
необходимости с дампом базы данных, в виде архива или ссылки на репозиторий с
решением.
Код должен быть оптимальным, читаемым для разработчика и удобным для
пользователя, при разработке желательно покрытие несколькими тестами и
использование общераспространенных практик (паттерны проектирования, KISS...)
Результат тестового задания должен содержать текстовый файл readme.txt с
инструкцией по настройке и конфигурированию приложения (если необходимо).

