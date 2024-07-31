<script setup>
    import { ref, watch, onMounted } from 'vue';
    import { useLayout } from '@/layout/composables/layout';
    import { CompanyFeedbackSurveyBackendReportService } from '@/service/CompanyFeedbackSurveyBackendReportService';

    const companyFeedbackSurveyBackendReportService = new CompanyFeedbackSurveyBackendReportService();
    const { layoutConfig } = useLayout();
    let documentStyle = getComputedStyle(document.documentElement);
    let textColor = documentStyle.getPropertyValue('--text-color');

    const filter = ref({
        campaignOptions: [],
        campaignId: -1,
        startTime: null,
        endTime: null
    });
    const pieData = ref(null);
    const pieOptions = ref({
        plugins: {
            legend: {
                labels: {
                    usePointStyle: true,
                    color: textColor
                }
            }
        }
    });
    const loading = ref(false);

    const setColorOptions = () => {
        documentStyle = getComputedStyle(document.documentElement);
        textColor = documentStyle.getPropertyValue('--text-color');
    };

    const setChart = async () => {
        pieData.value = {
            labels: ['1', '2', '3', 'SA'],
            datasets: [
                {
                    data: [540, 325, 702],
                    backgroundColor: [documentStyle.getPropertyValue('--indigo-500'), documentStyle.getPropertyValue('--purple-500'), documentStyle.getPropertyValue('--teal-500')]
                }
            ]
        };
    };

    onMounted( async () => {

    });

    watch(
        layoutConfig.theme,
        () => {
            setColorOptions();
            setChart();
        },
        { immediate: true }
    );
</script>

<template>
    <div class="grid p-fluid">
        <div class="col-3">
            <Dropdown v-model="filter.campaignId" :options="filter.campaignOptions" optionValue="value" optionLabel="text">
            </Dropdown>
        </div>
        <div class="col-1">
            <Calendar v-model="filter.startTime" />
        </div>
        <div class="col-1">
            <Calendar v-model="filter.endTime" />
        </div>
        <div>
            <Button type="button" severity="primary" class="mr-2 mb-2" label="Export" icon="pi pi-upload" :loading="loading" @click="onExportClick()" />
            <Button type="button" severity="success" class="mr-2 mb-2" label="Search" icon="pi pi-search" :loading="loading" @click="onSearchClick()" />
        </div>
    </div>
    <div class="grid p-fluid">
        <div class="col-12 xl:col-6">
            <div class="card flex flex-column align-items-center" v-for="question of data.questions">
                <h5 class="text-left w-full">{{question.displayOrderText}}. {{question.text}}</h5>
                <Chart type="pie" :data="question.pieData" :options="pieOptions"></Chart>
            </div>
        </div>
    </div>
</template>
