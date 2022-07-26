Shader "Custom/Sprites-Emission"{
  Properties{
    _Color ("Tint", Color) = (0, 0, 0, 1)
    _MainTex ("Texture", 2D) = "white" {}
  }

  SubShader{
    Tags{ 
      "RenderType"="Transparent" 
      "Queue"="Transparent"
    }

    Blend SrcAlpha OneMinusSrcAlpha

    ZWrite off
    Cull off

    Pass{

      CGPROGRAM

      #include "UnityCG.cginc"

      #pragma vertex vert
      #pragma fragment frag

      sampler2D _MainTex;
      float4 _MainTex_ST;

      fixed4 _Color;

      struct appdata{
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
        fixed4 color : COLOR;
      };

      struct v2f{
        float4 position : SV_POSITION;
        float2 uv : TEXCOORD0;
        fixed4 color : COLOR;
        float4 scr_pos : SCREEN_POSITION;
      };

      v2f vert(appdata v){
        v2f o;
        o.position = UnityObjectToClipPos(v.vertex);
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        o.color = v.color;
        o.scr_pos = ComputeScreenPos(o.position);
        return o;
      }

      fixed4 frag(v2f i) : SV_Target {
        fixed4 col = tex2D(_MainTex, i.uv);
        col *= _Color;
        col *= i.color;

        float2 wcoord = i.scr_pos.xy/i.scr_pos.w;
        // col *= clamp(3.0 * length(wcoord - 0.5), 0.5, 1.0);

        col *= wcoord.y;
        return col;
      }

      ENDCG
    }
  }
}