<script setup>
    import { ref, defineProps } from 'vue';
    const props = defineProps(['question']);
    const question = ref(props.question);
    const onAnswerChanged = () => {
        question.value.valid = true;        
    };
</script>

<template>
    <div class="grid" :id="'q' + question.id" :invalid="question.invalid && question.dirty">
        <div class="col-12">
            <Panel>
                <div class="grid">
                    <div class="col-7">
                        <p class="font-bold" :invalid="question.invalid && question.dirty">{{question.displayOrderText}}. {{question.text}}</p>
                    </div>
                    <div class="col-5">
                        <div class="flex flex-wrap gap-4" :invalid="!question.valid && question.dirty">
                            <div class="flex items-center" v-for="option of question.options" :key="option.id">                                
                                <label :for="'q'+ question.id + '_opt_' + option.id" class="ml-2">
                                    <RadioButton v-model="question.answerValue" @change="onAnswerChanged()" :invalid="!question.valid && question.dirty" :inputId="'q'+ question.id + '_opt_' + option.id" :value="option.id" /> {{option.description}}
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </Panel>
        </div>
    </div>
</template>

<style lang="scss" scoped>
</style>
