# Задача
Разработать приложение-калькулятор, которое позволит ввести математическое выражение и получить результат вычисления
# Backend
Модуль разбора выражений минимально должен поддерживать следующие операции:
- Сложение
- Вычитание
- Умножение
- Деление
- Скобки
В дополнение к собственному модулю разбора выражений, реализовать эту же функциональность с использованием стороннего сервиса (https://api.mathjs.org,
API Wolfram Alpha, и т.д. любой на выбор)

Язык программирования C# (.net5+)

Приложение должно иметь хорошую расширяемую архитектуру. Дополнительным плюсом также будет покрытие модуля разбора выражений юнит-тестами
# Frontend
Разработать React приложение, которое будет предоставлять возможность ввести математическое выражение и получить результат. 
Приложение должно иметь возможность выбрать провайдера вычислений (должны предоставляться backend частью).
Интерфейс калькулятора должен воспроизводить калькулятор Google https://www.google.com/search?q=calc за исключением неподдерживаемых функций.
В качестве CSS-framework можно использовать Bootstrap https://getbootstrap.com/docs/5.0/getting-started/introduction/

# Бонусное задание (не обязательно к выполнению)
Написать Dockerfile-ы для backend и frontend приложений. 
