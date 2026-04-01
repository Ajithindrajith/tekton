{{- define "front-chart.name" -}}
front
{{- end }}

{{- define "front-chart.fullname" -}}
{{ include "front-chart.name" . }}
{{- end }}

{{- define "front-chart.labels" -}}
app.kubernetes.io/name: {{ include "front-chart.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}