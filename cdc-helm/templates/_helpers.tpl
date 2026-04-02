{{- define "cdc-helm.name" -}}
cdc-helm
{{- end }}

{{- define "cdc-helm.fullname" -}}
{{ include "cdc-helm.name" . }}
{{- end }}

{{- define "cdc-helm.labels" -}}
app.kubernetes.io/name: {{ include "cdc-helm.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}