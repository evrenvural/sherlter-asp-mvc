// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function convertToJsDateFromDateTime(date) {
    const [day, month, year] = date.split(' ')[0].split('.');
    return `${year}-${month}-${day}`;
}

function getDogAge(dogBirthday) {
    const [birthdayMonth, birthdayYear] = moment(dogBirthday).format('MM/YYYY').split('/');
    const [todayMonth, todayYear] = moment(new Date()).format('MM/YYYY').split('/');

    if (todayYear - birthdayYear >= 1) return `${todayYear - birthdayYear} Yaşında`;

    return `${(todayMonth - birthdayMonth + 12) % 12} Aylık`;
}
