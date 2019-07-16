

function fixage(array) {
    var result = array.filter(elem => elem >= 18 && elem <= 60)
        .sort((item1, item2) => item1 - item2)
        .map(number => number.toString())
        .join(',');

    return result !== "" ? result : "NA";
}

/*
Nie wiem na jakiej zasadzie miałoby to warunkowo zwracać coś bez znajomości zdarzenia. Potrzebowalbym szerszego kontekstu
oraz przekazania zdarzenia z kontrolki
*/
function optionalPow(a, b) {
    return Math.pow(a, b);
}

