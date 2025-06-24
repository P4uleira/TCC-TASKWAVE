window.gerarGraficoBarras = (labels, data) => {
    const ctx = document.getElementById("graficoBarras");
    if (!ctx) return;

    if (window.myBarChart) {
        window.myBarChart.destroy();
    }

    window.myBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Minhas tarefas',
                data: data,
                backgroundColor: [
                    '#00FF00',
                    '#f6c23e',
                    '#ea80fc',
                    '#e74a3b',
                    '#6f42c1'
                ],
                barThickness: 30
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        color: 'white',
                        precision: 0
                    },
                    grid: {
                        drawOnChartArea: false,
                        drawTicks: false,
                        drawBorder: true,
                        color: 'white',
                        lineWidth: 2,
                        borderColor: 'white'
                    },
                    border: {
                        display: true,
                        color: 'white'
                    }
                },
                x: {
                    ticks: {
                        color: function (context) {
                            const cores = ['#00FF00', '#f6c23e', '#ea80fc', '#e74a3b', '#6f42c1'];
                            return cores[context.index] || 'white';
                        }
                    },
                    grid: {
                        drawOnChartArea: false,
                        drawTicks: false,
                        drawBorder: true,
                        color: 'white',
                        lineWidth: 2,
                        borderColor: 'white'
                    },
                    border: {
                        display: true,
                        color: 'white'
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                }
            }
        }
    });
};

window.gerarGraficoPizza = (labels, data) => {
    const ctx = document.getElementById("graficoTarefas");
    if (!ctx) return;

    if (window.myPieChart) {
        window.myPieChart.destroy();
    }

    window.myPieChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: data,
                backgroundColor: ['#1cc88a', '#f6c23e', '#e74a3b'],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    labels: {
                        color: 'white',
                        font: {
                            family: 'Segoe UI',
                            size: 14,
                            weight: 'bold'
                        }
                    }
                }
            }
        }
    });
};
