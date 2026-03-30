{{- define "backend-chart.name" -}}
backend
{{- end }}

{{- define "backend-chart.fullname" -}}
{{ include "backend-chart.name" . }}
{{- end }}

{{- define "backend-chart.labels" -}}
app.kubernetes.io/name: {{ include "backend-chart.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}