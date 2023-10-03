'use strict';

document.addEventListener('DOMContentLoaded', function () {
    const checkbox = document.querySelector('input[type="checkbox"]');
    const element = document.body;

    checkbox.addEventListener('change', function () {
        element.classList.toggle("dark-mode");
    });
});

class CalculateTip {
    constructor() {
        this.decimalInput = document.getElementById('decimalInput');
        this.calculateButton = document.getElementById('calculateButton');
        this.percentageSlider = document.getElementById('percentageSlider');
        this.percentageValue = document.getElementById('percentageValue');
        this.resultTipElement = document.getElementById('resultTip');
        this.resultTotalElement = document.getElementById('resultTotal');
    }

    formatDecimalInput() {
        const input = this.decimalInput;
        input.value = input.value.replace(/[^0-9.]/g, '');

        const parts = input.value.split('.');
        if (parts.length > 1) {
            parts[1] = parts[1].substring(0, 2);
        }

        input.value = parts.join('.');
    }

    updatePercentageSlider() {
        const slider = this.percentageSlider;
        const percentageValue = this.percentageValue;

        const percentage = slider.value;
        percentageValue.textContent = `${percentage}%`;
    }

    updateTipAmount() {
        const input = this.decimalInput;
        const inputValue = parseFloat(input.value) || 0;
        const percentage = parseFloat(this.percentageSlider.value) || 0;

        const calculatedTip = (percentage / 100) * inputValue;
        const calculatedTotal = inputValue + calculatedTip;

        this.resultTipElement.textContent = `${percentage}% Tip: $${calculatedTip.toFixed(2)}`;
        this.resultTotalElement.textContent = `Total: $${calculatedTotal.toFixed(2)}`;
    }

    attachEventListeners() {
        this.calculateButton.addEventListener('click', () => {
            this.updateTipAmount();
        });

        this.decimalInput.addEventListener('input', () => {
            this.formatDecimalInput();
        });

        this.percentageSlider.addEventListener('input', () => {
            this.updatePercentageSlider();
        });
    }
}

// Create an instance of the CalculateTip class
const calculateTip = new CalculateTip();

// Attach event listeners after the DOM has loaded
document.addEventListener('DOMContentLoaded', () => {
    calculateTip.attachEventListeners();
});
