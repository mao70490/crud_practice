function formatPrice(input) {
    let value = input.value.replace(/,/g, '');
    if (value === '' || isNaN(value)) return;

    const parts = parseFloat(value).toFixed(2).split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    input.value = parts.join('.');
}

function cleanPriceFormat() {
    const input = document.getElementById('Price');
    input.value = input.value.replace(/,/g, '');
}