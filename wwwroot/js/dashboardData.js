document.addEventListener('DOMContentLoaded', function () {
    const rawData = document.getElementById('chart-data');
    const rawDataAcces = document.getElementById('chart-data-acces');

    try {
        // Initialize chartData with empty values
        window.chartData = {
            labels: [],
            data: [],
            backgroundColors: [],
            champLabels: [],
            autoriseData: [],
            nonAutoriseData: [],
            dateConnexion: [],
            application: []
        };

        // Parse main chart data if available
        if (rawData) {
            const dataObj = JSON.parse(rawData.textContent);
            window.chartData = {
                ...window.chartData,
                labels: dataObj.labels || [],
                data: dataObj.data || [],
                backgroundColors: dataObj.backgroundColors || [],
                champLabels: (dataObj.champLabels || []).flat ? (dataObj.champLabels || []).flat() : [],
                autoriseData: dataObj.autoriseData || [],
                nonAutoriseData: dataObj.nonAutoriseData || []
            };
        }

        // Parse access chart data if available
        if (rawDataAcces) {
            const dataObjAcces = JSON.parse(rawDataAcces.textContent);
            window.chartData = {
                ...window.chartData,
                dateConnexion: dataObjAcces.dateConnexion || [],
                application: dataObjAcces.application || []
            };
        }
    } catch (error) {
        console.error("Error parsing chart data:", error);
        window.chartData = {
            labels: [],
            data: [],
            backgroundColors: [],
            champLabels: [],
            autoriseData: [],
            nonAutoriseData: [],
            dateConnexion: [],
            application: []
        };
    }
});