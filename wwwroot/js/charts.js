// Droits par groupe et par profil - Configuration et initialisation des graphiques
document.addEventListener('DOMContentLoaded', function () {

    const {
        labels,
        data,
        backgroundColors,
        champLabels,
        autoriseData,
        nonAutoriseData,
        dateConnexion,
        application
    } = window.chartData || {};

    if (!labels || !data) {
        console.warn("Chart data is missing required properties for basic charts");
    }

    // Configuration commune pour tous les graphiques

    const commonConfig = {
        responsive: true,
        borderWidth: 1,
        borderColor: 'rgba(0, 0, 0, 0.1)',
    };

    // Couleurs standard pour les statuts
    const statusColors = {
        autorise: {
            background: 'rgba(75, 192, 192, 0.7)',
            border: 'rgba(75, 192, 192, 1)'
        },
        nonAutorise: {
            background: 'rgba(255, 99, 132, 0.7)',
            border: 'rgba(255, 99, 132, 1)'
        },
        nonRenseigne: {
            background: 'rgba(201, 203, 207, 0.7)',
            border: 'rgba(201, 203, 207, 1)'
        }
    };

    // Fonction pour créer un graphique
    function createChart(elementId, chartConfig) {
        const element = document.getElementById(elementId);
        if (!element) {
            console.warn(`Element with ID ${elementId} not found`);
            return null;
        }
        const ctx = element.getContext('2d');
        return new Chart(ctx, chartConfig);
    }

    // Configuration du graphique "Droits par groupe" - VERTICAL
    if (labels && data) {
        const droitGroupeConfig = {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Statut',
                    data: data,
                    backgroundColor: backgroundColors,
                    borderColor: commonConfig.borderColor,
                    borderWidth: commonConfig.borderWidth
                }]
            },
            options: {
                // Suppression de indexAxis: 'y' pour avoir des barres verticales
                plugins: {
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const value = context.raw;
                                if (value === 1) return 'Autorisé';
                                if (value === -1) return 'Non autorisé';
                                return 'Non renseigné';
                            }
                        }
                    },
                    legend: {
                        display: false
                    }
                },
                responsive: commonConfig.responsive,
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: 'Applications'
                        },
                        ticks: {
                            maxRotation: 45,
                            minRotation: 45
                        }
                    },
                    y: {
                        display: false, // Garde l'échelle Y cachée comme avant
                        beginAtZero: true
                    }
                }
            }
        };
        createChart('droitGroupeChart', droitGroupeConfig);
    }

    // Configuration du graphique "Droits par profil" - HORIZONTAL
    if (champLabels && autoriseData && nonAutoriseData) {
        const droitProfilConfig = {
            type: 'bar',
            data: {
                labels: champLabels,
                datasets: [
                    {
                        label: 'Autorisé',
                        data: autoriseData,
                        backgroundColor: statusColors.autorise.background,
                        borderColor: statusColors.autorise.border,
                        borderWidth: commonConfig.borderWidth
                    },
                    {
                        label: 'Non autorisé',
                        data: nonAutoriseData,
                        backgroundColor: statusColors.nonAutorise.background,
                        borderColor: statusColors.nonAutorise.border,
                        borderWidth: commonConfig.borderWidth
                    }
                ]
            },
            options: {
                indexAxis: 'y', // Ligne pour avoir des barres horizontales
                plugins: {
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                return context.dataset.label + ': ' + context.raw + ' champ(s)';
                            }
                        }
                    }
                },
                responsive: commonConfig.responsive,
                scales: {
                    x: {
                        stacked: true,
                        title: {
                            display: true,
                            text: 'Droits'
                        },
                        ticks: {
                            display: false
                        },
                        grid: {
                            display: false // Supprimer les lignes de grille
                        }
                    },
                    y: {
                        stacked: true,
                        title: {
                            display: true,
                            text: 'Champs'
                        },
                    }
                }
            }
        };
        createChart('droitProfilChart', droitProfilConfig);
    }

    // Configuration du graphique "Acces aux applications" - HORIZONTAL
    if (dateConnexion && application && dateConnexion.length > 0 && application.length > 0) {
        try {
            const now = new Date();
            const twoMonthsAgo = new Date(now.getTime() - 60 * 24 * 60 * 60 * 1000);

            // 1. Génère les 30 jours (labels)
            const days = Array.from({ length: 60 }, (_, i) => {
                const d = new Date(twoMonthsAgo.getTime() + i * 24 * 60 * 60 * 1000);
                return d.toISOString().split('T')[0]; // format: 'YYYY-MM-DD'
            });

            // 2. Compte les connexions par jour et par application
            const accessByAppAndDay = {};

            application.forEach((app, index) => {
                const date = new Date(dateConnexion[index]);
                const day = date.toISOString().split('T')[0];

                if (date >= twoMonthsAgo && date <= now) {
                    if (!accessByAppAndDay[app]) accessByAppAndDay[app] = {};
                    accessByAppAndDay[app][day] = (accessByAppAndDay[app][day] || 0) + 1;
                }
            });

            // 3. Crée un dataset par application avec une couleur unique
            const datasets = Object.entries(accessByAppAndDay).map(([appName, counts], i) => ({
                label: appName,
                data: days.map(day => counts[day] || 0),
                backgroundColor: `hsl(${(i * 60) % 360}, 70%, 60%)`,
                borderColor: `hsl(${(i * 60) % 360}, 70%, 40%)`,
                fill: false,
                tension: 0.1
            }));

            // 4. Configuration du graphique (type ligne ou bar)
            if (datasets.length > 0) {
                const reaAccesConfig = {
                    type: 'line', // ou 'bar' si tu préfères
                    data: {
                        labels: days,
                        datasets: datasets
                    },
                    options: {
                        plugins: {
                            title: {
                                display: true,
                                text: 'Accès aux applications (60 derniers jours)'
                            },
                            tooltip: {
                                mode: 'index',
                                intersect: false,
                            }
                        },
                        responsive: true,
                        scales: {
                            y: {
                                beginAtZero: true,
                                title: {
                                    display: true,
                                    text: 'Nombre d\'accès'
                                }
                            },
                            x: {
                                title: {
                                    display: true,
                                    text: 'Date'
                                },
                                ticks: {
                                    maxRotation: 45,
                                    minRotation: 45
                                }
                            }
                        }
                    }
                };
                const reaAccesElement = document.getElementById('reaAccesChart');
                if (reaAccesElement) {
                    createChart('reaAccesChart', reaAccesConfig);
                } else {
                    console.warn("Element with ID 'reaAccesChart' not found");
                }
            }
        } catch (error) {
            console.error("Error creating access chart:", error);
        }
    }
});