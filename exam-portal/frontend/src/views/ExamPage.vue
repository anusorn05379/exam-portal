<script setup>


import { useRouter } from 'vue-router'
import CandidateForm from '../components/CandidateForm.vue'
import QuestionCard from '../components/QuestionCard.vue'
import { useExamStore } from '../stores/exam'
import { computed, onMounted } from 'vue'

const store = useExamStore()
const router = useRouter()

const fullName = computed({
    get: () => store.fullName,
    set: (value) => store.setFullName(value)
})

onMounted(async () => {
    if (!store.questions.length) {
        await store.loadQuestions()
    }
})

const handleSubmit = async () => {
    console.log('[ExamPage] handleSubmit clicked');
    if (!fullName.value.trim()) {
        console.log('[ExamPage] fullName is empty');
        alert('กรุณากรอกชื่อ-สกุลก่อนส่งคำตอบ')
        return
    }   
    const unanswered = store.questions.some((q) => !store.selectedAnswers[q.id])
    if(unanswered){
         console.log('[ExamPage] some questions are unanswered');
         alert('กรุณาตอบคำถามให้ครบทุกข้อ')
    return
    }

    try {
        console.log('[ExamPage] calling store.submit()');
        await store.submit()
        console.log('[ExamPage] store.submit() success, redirecting to /result');
        router.push('/result')
    } catch (error) {
        console.log('[ExamPage] submit failed with error:', error);
        alert('เกิดข้อผิดพลาดในการส่งคำตอบ: ' + error.message)
    }
}

</script>



<template>
  <div class="page">
    <div class="card">
      <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
        <h1 style="margin: 0;">IT 10-1</h1>
        <!-- <button style="border: 1px solid #ccc; background: white; padding: 6px 12px; border-radius: 4px; cursor: pointer; font-size: 14px;" @click="router.push('/history')">
          ดูประวัติการสอบทั้งหมด
        </button> -->
      </div>

      <CandidateForm v-model="fullName" />

      <div v-if="store.loading" class="info-text">กำลังโหลดข้อสอบ...</div>
      <div v-else-if="store.error" class="error-text">{{ store.error }}</div>

      <template v-else>
        <QuestionCard
          v-for="(question, index) in store.questions"
          :key="question.id"
          :question="question"
          :index="index + 1"
          :model-value="store.selectedAnswers[question.id]"
          @update:model-value="(value) => store.setAnswer(question.id, value)"
        />

        <button class="primary-btn" :disabled="store.loading" @click="handleSubmit">
          ส่งคำตอบ
        </button>
      </template>
    </div>
  </div>
</template>