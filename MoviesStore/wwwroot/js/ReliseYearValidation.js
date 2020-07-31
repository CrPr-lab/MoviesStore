// Добавляем атрибуты, за которые будут цепляться стандартные скрипты клиентской проверки
(() => {
    const reliseYear = document.getElementById("ReliseYear");
    reliseYear.setAttribute("data-val-range-max", new Date().getFullYear());
    reliseYear.setAttribute("data-val-range-min", "1895");
    reliseYear.setAttribute("data-val-range", "Год выхода должен быть в диапозоне 1895 - " + new Date().getFullYear());
} )() ;

