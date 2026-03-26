<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { getExamResult } from '../services/examService'
import { useExamStore } from '../stores/exam'

const route = useRoute()
const router = useRouter()
const store = useExamStore()
const resultDetail = ref(null)
const loading = ref(true)
const error = ref('')

onMounted(async () => {
    const id = route.params.id
    if (!id) {
        error.value = 'ไม่พบรหัสการสอบ'
        loading.value = false
        return
    }

    try {
        const data = await getExamResult(id)
        resultDetail.value = data
    } catch (err) {
        error.value = err.response?.data || 'ไม่พบข้อมูลผลสอบ หรือเกิดข้อผิดพลาด'
    } finally {
        loading.value = false
    }
})

const goHome = () => {
    store.resetExam()
    router.push('/')
}
</script>

<template>
  <div class="page">
    <div class="card">
      <h1>รายละเอียดผลสอบ</h1>

      <div v-if="loading" class="info-text">
        กำลังโหลดข้อมูล...
      </div>

      <div v-else-if="error" class="info-text" style="color: red;">
        {{ error }}
        <div style="margin-top: 20px;">
          <button class="primary-btn" @click="goHome">กลับหน้าหลัก</button>
        </div>
      </div>

      <template v-else-if="resultDetail">
        <div class="result-box">
          <p class="result-name">ชื่อ-สกุล: <strong>{{ resultDetail.fullName }}</strong></p>
          <p class="result-score">คะแนนรวม: <strong>{{ resultDetail.answers.filter(a => a.isCorrect).reduce((sum, a) => sum + a.score, 0) }}/{{ resultDetail.totalQuestions }}</strong></p>
          <p class="info-text">เวลาที่ส่งคำตอบ: {{ new Date(resultDetail.submittedAt).toLocaleString('th-TH') }}</p>
        </div>

        <div style="margin-top: 24px;">
          <h2 style="margin-bottom: 16px; font-size: 20px;">รายละเอียดการตอบ</h2>
          
          <div 
            v-for="(ans, index) in resultDetail.answers" 
            :key="index" 
            class="question-card"
            :style="{ borderColor: ans.isCorrect ? '#86efac' : '#fca5a5', backgroundColor: ans.isCorrect ? '#f0fdf4' : '#fef2f2' }"
          >
            <p class="question-title">ข้อที่ {{ ans.questionId }}</p>
            <div class="choice-item">
              สถานะ: 
              <strong :style="{ color: ans.isCorrect ? '#16a34a' : '#dc2626' }">
                {{ ans.isCorrect ? 'ตอบถูก' : 'ตอบผิด' }}
              </strong>
            </div>
            <div class="choice-item" v-if="ans.score > 0">
              คะแนนที่ได้: <strong>+{{ ans.score }}</strong>
            </div>
          </div>
        </div>

        <button class="primary-btn" style="margin-top: 24px; width: 100%;" @click="goHome">
          กลับไปทำข้อสอบใหม่
        </button>
      </template>
    </div>
  </div>
</template>