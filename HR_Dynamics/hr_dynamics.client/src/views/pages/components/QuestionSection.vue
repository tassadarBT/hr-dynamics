<script setup>
import { ref, defineProps } from 'vue';
import RadioListQuestion from './RadioListQuestion.vue';
const props = defineProps(['question']);
const section = ref(props.question);
</script>

<template>
    <div class="grid">
        <div class="col-12">
            <Panel toggleable>
                <template #header>
                    <div class="flex items-center gap-2">
                        <span class="font-bold">{{section.displayOrder}}. {{section.text}}</span>
                    </div>
                </template>
                <div v-for="question of section.questions">
                    <radio-list-question v-if="question.type == 'radio-list'" :question="question"></radio-list-question>
                </div>
            </Panel>
        </div>
    </div>    
</template>

<style lang="scss" scoped>

</style>
