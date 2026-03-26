
<script setup>
defineProps({
  result: {
    type: Object,
    required: true
  }
})
</script>


<template>
  <div class="result-box">
    <p class="result-name">คุณ {{ result.fullName }}</p>
    <p class="result-score">สอบได้คะแนน : {{ result.score }}/{{ result.totalQuestions }}</p>
    <p class="result-message">{{ result.message }}</p>
  </div>
</template>