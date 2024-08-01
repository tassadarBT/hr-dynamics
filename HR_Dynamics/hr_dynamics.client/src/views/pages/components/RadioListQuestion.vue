<script setup>
import { ref, defineProps } from 'vue';
const props = defineProps(['question']);
const question = ref(props.question);

const onAnswerChanged = () => {
    question.value.valid = true;
};

</script>

<template>
    <div class="grid" :id="'q' + question.id" :invalid="!question.valid && question.dirty">
        <div class="xl:col-7 col-12">
            <p class="fw-bold p-1" :invalid="!question.valid && question.dirty">{{question.displayOrderText}}. {{question.text}}</p>
        </div>
        <div class="xl:col-5 col-12">
            <div class="flex flex-wrap gap-4 p-1" :invalid="!question.valid && question.dirty">
                <div class="flex items-center" v-for="option of question.options" :key="option.id">                   
                    <label :for="'q'+ question.id + '_opt_' + option.id" class="ml-2">
                        <RadioButton v-model="question.answerValue" @change="onAnswerChanged()" :invalid="!question.valid && question.dirty" :inputId="'q'+ question.id + '_opt_' + option.id" :value="option.id" /> {{option.description}}
                    </label>
                </div>
            </div>
        </div>       
    </div>
</template>

<style lang="scss" scoped>

</style>
