document.querySelectorAll('input[name^="ProductQuantities"]').forEach(input => {
        input.addEventListener('input', () => {
            let totaal = 0;
            document.querySelectorAll('input[name^="ProductQuantities"]').forEach(i => totaal += (parseInt(i.value) || 0) * parseFloat(i.dataset.price));
            document.getElementById('totaal').textContent = '€' + totaal.toFixed(2).replace('.', ',');
        });
});