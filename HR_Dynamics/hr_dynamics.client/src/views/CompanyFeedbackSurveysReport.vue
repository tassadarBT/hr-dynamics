<script setup>
    import { ref, watch, onMounted } from 'vue';
    import { useLayout } from '@/layout/composables/layout';
    import { CompanyFeedbackSurveyBackendReportService } from '@/service/CompanyFeedbackSurveyBackendReportService';
    import { LoginService } from '@/service/LoginService.js';
    import { useRouter } from 'vue-router';

    const router = useRouter();
    const loginService = new LoginService();
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
    const data = ref({
        questions: []
    });
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
    const exporting = ref(false);

    const setColorOptions = () => {
        documentStyle = getComputedStyle(document.documentElement);
        textColor = documentStyle.getPropertyValue('--text-color');
    };

    const getFilter = async () => {
        filter.value = await (await companyFeedbackSurveyBackendReportService.getFilterData()).json();
    };

    const searchData = async () => {
        loading.value = true;
        data.value = await (await companyFeedbackSurveyBackendReportService.getReportData(filter.value)).json();
        loading.value = false;
    };

    const exportData = async () => {
        exporting.value = true;
        const res = await (await companyFeedbackSurveyBackendReportService.exportReportData(filter.value)).blob();
        const url = window.URL.createObjectURL(res);
        const a = document.createElement('a');
        a.href = url;
        var datetime = `${new Date().getFullYear()}-${new Date().getMonth() + 1}-${new Date().getDate()} ${new Date().getHours()}:${new Date().getMinutes()}:${new Date().getSeconds()}`;
        a.download = `Company_Feedback_Survey_${datetime}.xlsx`;
        document.body.append(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url);
        exporting.value = false;
    };
    const onSearchClick = async () => {
        await searchData();
    };
    
    const onExportClick = async () => {
        await exportData();
    };

    onMounted(async () => {
        if (!await loginService.isLoggedIn()) {
            router.push({ path: '/auth/login' });
        }
        await getFilter();
        await searchData();
    });

    watch(
        layoutConfig.theme,
        () => {
            setColorOptions();
        },
        { immediate: true }
    );
</script>

<template>
    <div class="grid p-fluid" v-if="filter">
        <div class="col-3">
            <Dropdown v-model="filter.campaignId" :options="filter.campaignOptions" optionValue="value" optionLabel="text">
            </Dropdown>
        </div>
        <div class="col-2">
            <Calendar v-model="filter.startTime" />
        </div>
        <div class="col-2">
            <Calendar v-model="filter.endTime" />
        </div>
        <div class="col-1">
            <Button type="button" severity="primary" class="mr-2 mb-2" label="Export" icon="pi pi-upload" :loading="exporting" @click="onExportClick()" />
        </div>
        <div class="col-1">
            <Button type="button" severity="success" class="mr-2 mb-2" label="Search" icon="pi pi-search" :loading="loading" @click="onSearchClick()" />
        </div>
    </div>
    <div class="grid p-fluid">
        <div class="col-12 xl:col-6" v-for="question of data.questions" :key="question.id">
            <div class="card flex flex-column align-items-center" style="height: 450px;">
                <div class="card-hea"></div>
                <h5 class="text-left w-full" style="height: 50px;">{{question.displayOrderText}}. {{question.text}}</h5>
                <Chart type="pie" :data="question.answerPieChartData" :options="pieOptions"></Chart>
            </div>
        </div>
    </div>
</template>
