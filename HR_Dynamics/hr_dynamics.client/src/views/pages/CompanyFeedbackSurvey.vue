<script setup>
    import { ref, computed, onMounted } from 'vue';
    import { useRouter } from 'vue-router';
    import { useToast } from 'primevue/usetoast';
    import { useLayout } from '@/layout/composables/layout';
    import { CompanyFeedbackSurveyService } from '@/service/CompanyFeedbackSurveyService';
    import QuestionSection from './components/QuestionSection.vue';
    import RadioListSectionQuestion from './components/RadioListSectionQuestion.vue';

    const router = useRouter();
    const smoothScroll = (id) => {
        document.getElementById(id).scrollIntoView({
            behavior: 'smooth'
        });
    };
    const { layoutConfig } = useLayout();
    const logoUrl = computed(() => {
        return `/layout/images/${layoutConfig.darkTheme.value ? 'logo-white' : 'logo-dark'}.svg`;
    });
    const toast = useToast();
    const companyFeedbackSurveyService = new CompanyFeedbackSurveyService();
    const data = ref(null);

    onMounted(async () => {
        data.value = await (await companyFeedbackSurveyService.getSurveyData()).json();
    });

    const setDirtyData = () => {
        for (let q of data.value.sections) {
            q.dirty = true;
            if (q.questions) {
                for (let sq of q.questions) {
                    sq.dirty = true;
                }
            }
        }
    };

    const findFirstInvalidQuestion = () => {
        for (let q of data.value.sections) {
            if (!q.questions || q.questions.length == 0) {
                if (!q.valid) {
                    return q;
                }
            }
            else 
            {
                for (let sq of q.questions) {
                    if (!sq.valid) {
                        return sq;
                    }
                }
            }
        }
        return null;
    };

    const onSaveClick = async () => {
        setDirtyData();
        const firstQuestionUnAnswered = findFirstInvalidQuestion();
        if (firstQuestionUnAnswered) {
            smoothScroll('q' + firstQuestionUnAnswered.id);
        }
        else {            
            const resSave = await (await companyFeedbackSurveyService.saveSurveyData(data.value)).json();
            if (resSave.success) {
                data.value.submitted = true;
            } else {
                toast.add({ severity: 'error', summary: 'Save Failure', detail: resSave.errorMessage, life: 3000 });
            }
        }
    };
</script>
<template>
    <div class="layout-topbar">
        <router-link to="/landing" class="layout-topbar-logo" style="width: auto;">
            <img :src="logoUrl" alt="logo" style="height: 75px;" />
        </router-link>
        <h4>Chestionar de satisfactie a personalului</h4>
        <div class="layout-topbar-menu">
            <Button icon="pi pi-check" severity="success" label="Save" @click="onSaveClick()" />
        </div>
    </div>
    <form style="padding-top: 75px;" v-if="data && !data.submitted">
        <div className="card" style="user-select: none; padding: 10px;" >
            <table>
                <tbody>
                    <tr>
                        <td style="vertical-align: top; width:440px;">
                            <i class="pi pi-fw pi-question-circle" style="font-size: 20px;"></i> Acest chestionar este facultativ si anonim. El are ca obiectiv:
                        </td>
                        <td style="vertical-align: top;">
                            <ul style="margin-top: 0; margin-left: 0; padding-left: 20px; margin-bottom: 0;">
                                <li>sa masoare nivelul de satisfactie a intregului personal al intreprinderii.</li>
                                <li>sa identifice masurile de intreprins pentru a ameliora nivelul de satisfactie a personalului.</li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Chestionarul cuprinde 18 intrebari  la care trebuie sa raspundeti  in mod obiectiv bifand una din evaluarile de mai jos.Va rugam sa predati chestionarul sefului ierarhic inainte de ______________
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Evaluare:   <b>1</b> =  nemultumit,	<b>2</b> = nici satisfacut / nici nemultumit, <b>3</b> = satisfacut,	<b>SA</b> = fara nici o parere
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-for="section of data.sections">
            <question-section v-if="section.type == 'section'" :question="section"></question-section>
            <radio-list-section-question v-if="section.type == 'section-radio-list'" :question="section"></radio-list-section-question>
        </div>   
        <div class="grid">
            <div class="col-12">
                <div class="field">
                    <label for="notes"> Aveti alte nevoi sau asteptari la care Leman Industrie ar trebui sa raspunda? Daca da, va rugam sa le enumerati in spatiul de mai jos.</label>
                    <Textarea v-model="data.notes" variant="filled" rows="5" style="width: 100%;" />
                </div>
            </div>
        </div>
        <div class="grid">
            <div class="col-2">
                <div class="field">
                    <label for="site">Site</label>
                    <div>
                        <InputText id="site" type="text" v-model="data.site" />
                    </div>
                </div>
            </div>
            <div class="col-2">
                <div class="field">
                    <label for="department">Departament</label>
                    <div>
                        <InputText id="department" type="text" v-model="data.department" />
                    </div>
                </div>
            </div>
            <div class="col-8">
                <div class="field">
                    <label>Access</label>
                    <div class="flex items-center">
                        <input type="checkbox" v-model="data.indirectAccess" id="indirectAccess" />
                        <label for="indirectAccess">Indirect : sefi ateliere, responsabili calitate, sefi de proiecte, metrologie, personal auxiliar etc.</label>
                    </div>
                    <div class="flex items-center">
                        <input type="checkbox" v-model="data.directAccess" id="directAccess" />
                        <label for="directAccess">Direct : reglori, sculeri- matriteri, controlori, sefi de echipa, mentenanta, operatori etc.</label>
                    </div>
                </div>
            </div>
        </div>        
    </form>
    <div style="padding-top: 75px;" v-if="data && data.submitted">
        <div className="card" style="user-select: none; padding: 10px;">
            <p>Va multumim pt timpul acordat! Formularul a fost trimis cu success!</p>
        </div> 
    </div>
</template>
