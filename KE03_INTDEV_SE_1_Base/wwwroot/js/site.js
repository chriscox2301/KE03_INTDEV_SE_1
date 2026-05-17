document.querySelectorAll('input[name="SelectedProductIds"]').forEach(cb => {
    cb.addEventListener('change', () => {
        let totaal = 0;
        document.querySelectorAll('input[name="SelectedProductIds"]:checked')
            .forEach(c => totaal += parseFloat(c.dataset.price));
        document.getElementById('totaal').textContent = '€' + totaal.toFixed(2).replace('.', ',');
    });
});