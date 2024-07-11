<script setup>
    import { ref, computed } from 'vue';
    import { useRouter } from 'vue-router';
    import { useLayout } from '@/layout/composables/layout';
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
    
    const opts = [{ id: 1, description: '1' }, { id: 2, description: '2' }, { id: 3, description: '3' }, { id: -1, description: 'SA' }];
    const data = ref({
        submitted : false,
        notes: null,
        directAccess: false,
        indirectAccess: false,
        department: null,
        site: null,
        questions: [
            {
                displayOrder: 'A',
                text: 'Cum evaluati atmosfera la locul de munca:',
                type: 'section',
                class: 'fw-bold',
                questions: [
                    { 'displayOrder': 'A1', text: '- in companie?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                    { 'displayOrder': 'A2', text: '- in atelierul/departamentul in care lucrati?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                ]
            },
            {
                displayOrder: 'B',
                text: 'Cum evaluati colaborarea:',
                type: 'section',
                questions: [
                    { 'displayOrder': 'B1', text: '- cu colegii de munca?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                    { 'displayOrder': 'B2', text: ' - cu managerii?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false, dirty: false, valid: false }
                ]
            },
            {
                displayOrder: 'C',
                type: 'section',
                text: 'Cum evaluati locul  dumneavoastra de munca in ceea ce priveste:',
                questions: [
                    { 'displayOrder': 'C1', text: '- organizarea si eficacitatea muncii in intreprindere?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                    { 'displayOrder': 'C2', text: '- echipamentele(masinile) si mijloacele logistice(calculatoare, programe informatice, telefon, fax...) puse la dispozitie?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                    { 'displayOrder': 'C3', text: '- comunicarea si transparenta in ceea ce priveste politica intreprinderii, abordarea problemelor, discutarea chestiunilor sociale', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                    { 'displayOrder': 'C4', text: '- mediul social (sala de mese, toalete, sala de sedinte, spatii verzi, etc) ?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                    { 'displayOrder': 'C5', text: '- amenajarea locului dvs.de lucru(amplasare, ergonomie, aerisire, iluminat, caldura/ umiditate, zgomot, vibratii, poluare)?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false },
                    { 'displayOrder': 'C6', text: '- securitatea si igiena muncii in serviciul dvs. (securitatea masinilor, echipamente de protectie individuale si colective, dispozitii privind securitatea  si curatenia la locul de munca)?', type: 'radio-list', options: opts, required: true, dirty: false, valid: false }
                ]
            },
            { displayOrder: 'D', text: 'Cum evaluati atitudinea sefilor  dumeavoastra ierarhici privind necesitatile dvs., cererile si propunerile (privind productivitatea, calitatea, mediul inconjurator, securitatea, formarile de care aveti nevoie,  diverse nelamuriri?', type: 'section-radio-list', options: opts, required: true, dirty: false, valid: false },
            { displayOrder: 'E', text: 'Sunteti satisfacut de nivelul de autonomie la locul de munca?', type: 'section-radio-list', options: opts, required: true, dirty: false, valid: false },
            { displayOrder: 'F', text: 'Fata de as teptarile dvs., considerati  ca ati progresat si acumulat  competente la Leman Industrie?', type: 'section-radio-list', options: opts, required: true, dirty: false, valid: false },
            { displayOrder: 'G', text: 'Va simtiti motivat sa va indepliniti obiectivele?', type: 'section-radio-list', options: opts, required: true, dirty: false, valid: false },
            { displayOrder: 'H', text: 'Pe parcursul unei saptamani obisnuite cum percepeti situatiile de stres si presiune ?', type: 'section-radio-list', options: opts, required: true, dirty: false, valid: false },
            { displayOrder: 'I', text: 'Cum simtiti ca sunteti remunerat pentru munca depusa? Mentionati in campul "Comentarii" tipurile de beneficii salariale pe care vi le doriti.', type: 'section-radio-list', options: opts, required: true, dirty: false, valid: false },
            { displayOrder: 'J', text: 'Cat de mandru sunteti de imaginea companiei la care lucrati? Mentionati in campul "Comentarii" ce apreciati cel mai mult la angajatorul Leman?', type: 'section-radio-list', options: opts, required: true, dirty: false, valid: false }
        ]
    });
    const setDirtyData = () => {
        for (let q of data.value.questions) {
            q.dirty = true;
            if (q.questions) {
                for (let sq of q.questions) {
                    sq.dirty = true;
                }
            }
        }
    };

    const findFirstInvalidQuestion = () => {
        for (let q of data.value.questions) {
            if (!q.questions) {
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

    const onSaveClick = () => {
        setDirtyData();
        const firstQuestionUnAnswered = findFirstInvalidQuestion();
        if (firstQuestionUnAnswered) {
            smoothScroll('q' + firstQuestionUnAnswered.displayOrder);
        }
        else {
            data.value.submitted = true;
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
    <form style="padding-top: 75px;" v-if="!data.submitted">
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
        <div v-for="question of data.questions">
            <question-section v-if="question.type == 'section'" :question="question"></question-section>
            <radio-list-section-question v-if="question.type == 'section-radio-list'" :question="question"></radio-list-section-question>
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
    <div style="padding-top: 75px;" v-if="data.submitted">
        <div className="card" style="user-select: none; padding: 10px;">
            <p>Va multumim pt timpul acordat! Formularul a fost trimis cu success!</p>
        </div> 
    </div>
</template>
