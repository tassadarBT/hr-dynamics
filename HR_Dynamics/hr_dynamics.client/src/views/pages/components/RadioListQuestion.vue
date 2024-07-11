<script setup>
import { ref, defineProps } from 'vue';
const props = defineProps(['question']);
const question = ref(props.question);

const onAnswerChanged = () => {
    question.value.valid = true;
};

</script>

<template>
    <div class="grid" :id="'q' + question.displayOrder" :invalid="!question.valid && question.dirty">
        <div class="col-7">
            <p class="fw-bold" :invalid="!question.valid && question.dirty">{{question.displayOrder}}. {{question.text}}</p>
        </div>
        <div class="col-5">
            <div class="flex flex-wrap gap-4" :invalid="!question.valid && question.dirty">
                <div class="flex items-center" v-for="option of question.options" :key="option.id">
                    <RadioButton v-model="question.answer" @change="onAnswerChanged()" :invalid="!question.valid && question.dirty"  :inputId="'q'+ question.displayOrder + '_opt_' + option.id" :value="option.id" />
                    <label :for="'q'+ question.displayOrder + '_opt_' + option.id" class="ml-2">{{option.description}}</label>
                </div>
            </div>
        </div>       
    </div>
</template>

<style lang="scss" scoped>

</style>
