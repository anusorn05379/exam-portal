<script setup>
defineProps({
  question: {
    type: Object,
    required: true
  },
  index: {
    type: Number,
    required: true
  },
  modelValue: {
    type: Number,
    default: null
  }
})

defineEmits(['update:modelValue'])
</script>

<template>
  <div class="question-card">
    <p class="question-title">{{ index }}. {{ question.text }}</p>

    <div
      v-for="choice in question.choices"
      :key="choice.id"
      class="choice-item"
    >
      <label>
        <input
          type="radio"
          :name="`question-${question.id}`"
          :value="choice.id"
          :checked="modelValue === choice.id"
          @change="$emit('update:modelValue', choice.id)"
        />
        <span>{{ choice.text }}</span>
      </label>
    </div>
  </div>
</template>
