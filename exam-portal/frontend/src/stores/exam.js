import { defineStore } from "pinia";
import { getQuestions, submitExam } from "../services/examService";

export const useExamStore = defineStore("exam", {
  state: () => ({
    fullName: "",
    questions: [],
    selectedAnswers: {},
    result: null,
    loading: false,
    error: "",
  }),
  actions: {
    async loadQuestions() {
      try {
        console.log('[Store] loadQuestions called');
        this.loading = true;
        this.error = "";
        this.questions = await getQuestions();
        console.log('[Store] loadQuestions success:', this.questions);
      } catch (error) {
        console.log('[Store] loadQuestions error:', error);
        this.error = error?.response?.data || "ไม่สามารถโหลดข้อสอบได้";
      } finally {
        this.loading = false;
      }
    },
    setFullName(Value) {
      console.log('[Store] setFullName:', Value);
      this.fullName = Value;
    },
    setAnswer(questionId, choiceId) {
      console.log('[Store] setAnswer:', { questionId, choiceId });
      this.selectedAnswers[questionId] = choiceId;
    },

    async submit() {
      try {
        console.log('[Store] submit called');
        this.loading = true;
        this.error = "";
        const answers = this.questions.map((question) => ({
          questionId: question.id,
          choiceId: this.selectedAnswers[question.id],
        }));

        const payload = {
          fullName: this.fullName,
          answers,
        };
        console.log('[Store] payload before submitExam API:', payload);
        this.result = await submitExam(payload);
        console.log('[Store] submit success, result:', this.result);
        return this.result;
      } catch (error) {
        console.log('[Store] submit error:', error);
        this.error = error?.response?.data || "ไม่สามารถส่งคำตอบได้";
        throw error;
      }
    },
    resetExam() {
      console.log('[Store] resetExam called');
      this.fullName = "";
      this.questions = [];
      this.selectedAnswers = {};
      this.result = null;
      this.error = "";
    },
  },
});
